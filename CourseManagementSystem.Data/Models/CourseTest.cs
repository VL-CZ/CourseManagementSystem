using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public CourseTest(ICollection<TestQuestion> questions) : this()
        {
            Questions = questions;
        }
    }
}
