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
        /// questions in this test
        /// </summary>
        public ICollection<TestQuestion> Questions { get; set; }

        public CourseTest() { }

        public CourseTest(ICollection<TestQuestion> questions) : this()
        {
            Questions = questions;
        }
    }
}
