using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class CourseTest
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// course that contains this test
        /// </summary>
        [Required]
        public Course Course { get; set; }

        /// <summary>
        /// questions in this test
        /// </summary>
        public ICollection<TestQuestion> Questions { get; set; }

        /// <summary>
        /// students' submissions of this test
        /// </summary>
        public ICollection<TestSubmission> Submissions { get; set; }

        public CourseTest()
        {
            Questions = new List<TestQuestion>();
            Submissions = new List<TestSubmission>();
        }

        public CourseTest(string topic, ICollection<TestQuestion> questions) : this()
        {
            Topic = topic;
            Questions = questions;
        }

        /// <summary>
        /// get question by its number
        /// </summary>
        /// <param name="test">given test</param>
        /// <param name="questionNumber">number of the question</param>
        /// <returns></returns>
        public TestQuestion GetQuestionByNumber(int questionNumber)
        {
            return Questions.Single(q => q.Number == questionNumber);
        }
    }
}
