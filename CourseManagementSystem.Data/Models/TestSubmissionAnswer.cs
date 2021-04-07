using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing one submitted answer of the test
    /// </summary>
    public class TestSubmissionAnswer : IGuidIdObject
    {
        public TestSubmissionAnswer()
        { }

        public TestSubmissionAnswer(TestQuestion question, string answerText) : this()
        {
            Question = question;
            Text = answerText;
        }

        /// <summary>
        /// identifier of the answer in a test submission
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// question which is answered
        /// </summary>
        public TestQuestion Question { get; set; }

        /// <summary>
        /// submitted text of the answer
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// points obtained for the answer
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// comment to the answer provided by a teacher
        /// </summary>
        public string Comment { get; set; }
    }
}