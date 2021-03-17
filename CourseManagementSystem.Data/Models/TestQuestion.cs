using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Data.Models
{
    public class TestQuestion
    {
        public TestQuestion() { }

        public TestQuestion(int number, string text, string correctAnswer, int points) : this()
        {
            Number = number;
            QuestionText = text;
            CorrectAnswer = correctAnswer;
            Points = points;
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// number of question
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// text of the question
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// correct answer to the question
        /// </summary>
        public string CorrectAnswer { get; set; }

        /// <summary>
        /// number of points for this question
        /// </summary>
        public int Points { get; set; }
    }
}
