using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// entity representing a question in the test
    /// </summary>
    public class TestQuestion : IGuidIdObject
    {
        public TestQuestion() { }

        /// <summary>
        /// create new question
        /// </summary>
        /// <param name="number">number of the question</param>
        /// <param name="text">text of the qeustion</param>
        /// <param name="correctAnswer">correct answer to the question</param>
        /// <param name="points">max. points for the question</param>
        /// <param name="questionType">type of the answers to the question</param>
        public TestQuestion(int number, string text, string correctAnswer, int points, QuestionType questionType) : this()
        {
            Number = number;
            QuestionText = text;
            CorrectAnswer = correctAnswer;
            Points = points;
            Type = questionType;
        }

        /// <summary>
        /// identifier of the question
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// number of question in the test
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// type of the question
        /// </summary>
        public QuestionType Type { get; set; }

        /// <summary>
        /// text of the question
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string QuestionText { get; set; }

        /// <summary>
        /// correct answer to the question
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string CorrectAnswer { get; set; }

        /// <summary>
        /// number of points for this question
        /// </summary>
        public int Points { get; set; }
    }
}
