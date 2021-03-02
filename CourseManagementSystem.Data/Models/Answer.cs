using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing one answer of the test
    /// </summary>
    public class Answer
    {
        public Answer()
        { }

        public Answer(TestQuestion question, string answerText) : this()
        {
            Question = question;
            Text = answerText;
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// question which is answered
        /// </summary>
        public TestQuestion Question { get; set; }

        /// <summary>
        /// submitted text of the answer
        /// </summary>
        public string Text { get; set; }
    }
}
