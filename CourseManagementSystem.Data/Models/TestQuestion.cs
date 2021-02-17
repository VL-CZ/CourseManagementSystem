using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class TestQuestion
    {
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

        public TestQuestion() { }

        public TestQuestion(int number, string text, string correctAnswer) : this()
        {
            Number = number;
            QuestionText = text;
            CorrectAnswer = correctAnswer;
        }
    }
}
