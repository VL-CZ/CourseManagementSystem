using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class TestQuestion
    {
        public int Number { get; set; }

        public string QuestionText { get; set; }

        public string CorrectAnswer { get; set; }

        public TestQuestion(int number, string text, string correctAnswer)
        {
            Number = number;
            QuestionText = text;
            CorrectAnswer = correctAnswer;
        }
    }
}
