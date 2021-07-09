using CourseManagementSystem.Data.Models;
using System;
using System.Linq;

namespace CourseManagementSystem.TestEvaluation
{
    /// <summary>
    /// evaluator of test submissions
    /// </summary>
    public class TestSubmissionEvaluator
    {
        /// <summary>
        /// evaluate the given test submission - set points to <see cref="TestSubmission.Answers"/>
        /// </summary>
        /// <param name="testSubmission">submission to evaluate</param>
        public void Evaluate(TestSubmission testSubmission)
        {
            foreach (TestSubmissionAnswer answer in testSubmission.Answers)
            {
                if (answer.Question.Type == Data.QuestionType.MultipleChoice)
                {
                    answer.Points = EvaluateMultipleChoiceAnswer(answer);
                }
                else
                {
                    answer.Points = EvaluateTextAnswer(answer);
                }
            }
        }

        /// <summary>
        /// evaluate text answer
        /// </summary>
        /// <param name="answer">answer to evaluate</param>
        /// <returns>calculated number of points for the answer</returns>
        private int EvaluateTextAnswer(TestSubmissionAnswer answer)
        {
            if (answer.Text == answer.Question.CorrectAnswer)
            {
                return answer.Question.Points;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// evaluate answer with multiple choices
        /// </summary>
        /// <param name="answer">answer to evaluate</param>
        /// <returns>calculated number of points for the answer</returns>
        private int EvaluateMultipleChoiceAnswer(TestSubmissionAnswer answer)
        {
            var correctAnswers = answer.Question.CorrectAnswer.Split(Config.answersDelimiter);
            var submittedAnswers = answer.Text.Split(Config.answersDelimiter);
            var allPossibleAnswers = GetAnswerLetters(answer.Question.QuestionText);

            int correct = 0;

            foreach (var possibleAnswer in allPossibleAnswers)
            {
                if (IsCorrect(possibleAnswer, correctAnswers, submittedAnswers))
                {
                    correct++;
                }
            }

            double correctRatio = (double)correct / allPossibleAnswers.Length;
            return (int)Math.Round(correctRatio * answer.Question.Points);
        }

        private bool IsCorrect(string answerLetter, string[] correctAnswers, string[] submittedAnswers)
        {
            var containedInBoth = correctAnswers.Contains(answerLetter) && submittedAnswers.Contains(answerLetter);
            var containedInNone = !correctAnswers.Contains(answerLetter) && !submittedAnswers.Contains(answerLetter);

            return containedInBoth || containedInNone;
        }

        private string[] GetAnswerLetters(string questionText)
        {
            var allPossibleAnswers = questionText.Split(Config.answersDelimiter);
            
            return allPossibleAnswers
                .Where((_, index) => index >= 1)
                .Select(answer => answer.Split(Config.answerLetterDelimiter)[0])
                .ToArray();
        }
    }
}
