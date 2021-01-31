using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class Answer
    {
        public int QuestionNumber { get; }

        public string Text { get; }

        public Answer(int questionNumber, string answerText)
        {
            QuestionNumber = questionNumber;
            Text = answerText;
        }
    }
}
