using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using System.Linq;

namespace CourseManagementSystem.TestEvaluation
{
    /// <summary>
    /// class for checking validity of <see cref="TestQuestion"/> type
    /// </summary>
    public class QuestionValidator
    {
        /// <summary>
        /// check if value of <see cref="TestQuestion.CorrectAnswer"/> property is valid
        /// </summary>
        /// <param name="question">question to check</param>
        /// <returns>TRUE - valid, FALSE - invalid</returns>
        public bool HasValidCorrectAnswer(TestQuestion question)
        {
            if (question.Type == QuestionType.TextAnswer)
            {
                return true;
            }
            else
            {
                var correctLetters = QuestionWithChoicesTools.GetCorrectAnswersLetters(question);
                var allChoicesLetters = QuestionWithChoicesTools.GetAnswerChoicesLetters(question);

                bool allAnswersValid = correctLetters.All(letter => allChoicesLetters.Contains(letter));
                bool choicesValid = AreChoicesValid(allChoicesLetters);

                if (question.Type == QuestionType.SingleChoice)
                {
                    return allAnswersValid && choicesValid && correctLetters.Length == 1;
                }
                else
                {
                    return allAnswersValid && choicesValid;
                }
            }
        }

        /// <summary>
        /// check if the all choices are valid (e.g. there are no 2 choices with the same letter, or choice with non-letter identifier,...)
        /// </summary>
        /// <param name="choicesIdentifiers">identifiers of the answer choices (typically uppercase letters)</param>
        /// <returns></returns>
        private bool AreChoicesValid(string[] choicesIdentifiers)
        {
            var distinctChoicesLetters = choicesIdentifiers.Distinct();
            var allLetters = choicesIdentifiers.All(choice => choice.Length == 1 && char.IsUpper(choice, 0));
            return allLetters && choicesIdentifiers.SequenceEqual(distinctChoicesLetters);
        }
    }
}