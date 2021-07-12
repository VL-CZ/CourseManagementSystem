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
                var possibleLetters = QuestionWithChoicesTools.GetAnswerChoicesLetters(question);

                bool allAnswersValid = correctLetters.All(letter => possibleLetters.Contains(letter));

                if (question.Type == QuestionType.SingleChoice)
                {
                    return allAnswersValid && correctLetters.Length == 1;
                }
                else
                {
                    return allAnswersValid;
                }
            }
        }
    }
}