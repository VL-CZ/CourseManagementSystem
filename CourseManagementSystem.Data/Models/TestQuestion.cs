using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    public class TestQuestion : IGuidIdObject
    {
        public TestQuestion() { }

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
