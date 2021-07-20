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
                    answer.Points = EvaluateTextualOrSingleChoiceAnswer(answer);
                }
            }
        }

        /// <summary>
        /// evaluate textual or single choice answer
        /// </summary>
        /// <param name="answer">answer to evaluate</param>
        /// <returns>calculated number of points for the answer</returns>
        private int EvaluateTextualOrSingleChoiceAnswer(TestSubmissionAnswer answer)
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
        /// evaluate answer with multiple answer choices
        /// </summary>
        /// <param name="answer">answer to evaluate</param>
        /// <returns>calculated number of points for the answer</returns>
        private int EvaluateMultipleChoiceAnswer(TestSubmissionAnswer answer)
        {
            var correctAnswers = QuestionWithChoicesTools.GetCorrectAnswersLetters(answer.Question);
            var submittedAnswers = QuestionWithChoicesTools.GetSubmittedAnswersLetters(answer);
            var allPossibleAnswers = QuestionWithChoicesTools.GetAnswerChoicesLetters(answer.Question);

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
        /// check if the answer choice with the given letter is correctly answered
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
    }
}
