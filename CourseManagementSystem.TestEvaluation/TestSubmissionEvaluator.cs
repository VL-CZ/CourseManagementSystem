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
                    answer.Points = EvaluateAnswer(answer);
                }
            }
        }

        /// <summary>
        /// evaluate text or single choice answer
        /// </summary>
        /// <param name="answer">answer to evaluate</param>
        /// <returns>calculated number of points for the answer</returns>
        private int EvaluateAnswer(TestSubmissionAnswer answer)
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
            var allPossibleAnswers = GetAnswerChoiceLetters(answer.Question.QuestionText);

            int correct = 0;

            foreach (var possibleAnswer in allPossibleAnswers)
            {
                if (IsAnswerLetterCorrect(possibleAnswer, correctAnswers, submittedAnswers))
                {
                    correct++;
                }
            }

            double correctRatio = (double)correct / allPossibleAnswers.Length;
            return (int)Math.Round(correctRatio * answer.Question.Points);
        }

        /// <summary>
        /// check if the answer choice with the given letter is correctly responded
        /// </summary>
        /// <param name="answerLetter">letter of the answer choice to check</param>
        /// <param name="correctAnswersLetters">letters of all correct answer choices</param>
        /// <param name="submittedAnswersLetters">letters of all submitted answer choices</param>
        /// <returns></returns>
        private bool IsAnswerLetterCorrect(string answerLetter, string[] correctAnswersLetters, string[] submittedAnswersLetters)
        {
            var containedInBoth = correctAnswersLetters.Contains(answerLetter) && submittedAnswersLetters.Contains(answerLetter);
            var containedInNone = !correctAnswersLetters.Contains(answerLetter) && !submittedAnswersLetters.Contains(answerLetter);

            return containedInBoth || containedInNone;
        }

        /// <summary>
        /// get all letters of answer choices
        /// </summary>
        /// <param name="questionText">serialized text of the question</param>
        /// <returns></returns>
        private string[] GetAnswerChoiceLetters(string questionText)
        {
            var allPossibleAnswers = questionText.Split(Config.answersDelimiter);
            
            return allPossibleAnswers
                .Where((_, index) => index >= 1)
                .Select(answer => answer.Split(Config.answerLetterDelimiter)[0])
                .ToArray();
        }
    }
}
