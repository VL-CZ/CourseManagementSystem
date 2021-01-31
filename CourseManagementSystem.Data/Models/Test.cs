using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class Test
    {
        public ICollection<TestQuestion> Questions { get; }

        public Test(ICollection<TestQuestion> questions)
        {
            Questions = questions;
        }
    }
}
