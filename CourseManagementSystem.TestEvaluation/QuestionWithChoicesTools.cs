using CourseManagementSystem.Data.Models;
using System;
using System.Linq;

namespace CourseManagementSystem.TestEvaluation
{
    /// <summary>
    /// class with additional methods for single and multiple choice questions
    /// </summary>
    public static class QuestionWithChoicesTools
    {
        /// <summary>
        /// delimiter of answer choices (each choice contains choice letter and text)
        /// </summary>
        public const string answerChoicesDelimiter = "|||";

        /// <summary>
        /// delimiter of the answer choice letter and answer choice text
        /// </summary>
        public const string answerChoiceLetterAndTextDelimiter = "||";

        /// <summary>
        /// delimiter between the letters of selected answer choices
        /// </summary>
        public const string answerLetterDelimiter = ",";

        /// <summary>
        /// get letters of all correct answers to the <paramref name="question"/>
        /// </summary>
        /// <param name="question">given question</param>
        /// <returns></returns>
        public static string[] GetCorrectAnswersLetters(TestQuestion question)
        {
            return question.CorrectAnswer.Split(answerLetterDelimiter, StringSplitOptions.RemoveEmptyEntries).TrimAll();
        }

        /// <summary>
        /// get letters of all submitted answer choices in the <paramref name="answer"/>
        /// </summary>
        /// <param name="answer">given answer</param>
        /// <returns></returns>
        public static string[] GetSubmittedAnswersLetters(TestSubmissionAnswer answer)
        {
            return answer.Text.Split(answerLetterDelimiter, StringSplitOptions.RemoveEmptyEntries).TrimAll();
        }

        /// <summary>
        /// get all letters of answer choices related to the <paramref name="question"/>
        /// </summary>
        /// <param name="question">question whose answer choices letters we want to obtain</param>
        /// <returns></returns>
        public static string[] GetAnswerChoicesLetters(TestQuestion question)
        {
            var allPossibleAnswers = question.QuestionText.Split(answerChoicesDelimiter).ToList();

            if (allPossibleAnswers.Count <= 1)
            {
                return new string[0];
            }

            // remove the first element - text of the question
            allPossibleAnswers.RemoveAt(0);

            return allPossibleAnswers
                .Select(answer => answer.Split(answerChoiceLetterAndTextDelimiter)[0])
                .ToArray()
                .TrimAll();
        }
    }

    /// <summary>
    /// simple class with extensions for type <see cref="string[]"/>
    /// </summary>
    public static class StringArrayExtensions
    {
        /// <summary>
        /// trim all strings from <paramref name="values"/>
        /// </summary>
        /// <param name="values">string values to trim</param>
        /// <returns>array of trimmed strings</returns>
        public static string[] TrimAll(this string[] values)
        {
            return values.Select(item => item.Trim()).ToArray();
        }
    }
}