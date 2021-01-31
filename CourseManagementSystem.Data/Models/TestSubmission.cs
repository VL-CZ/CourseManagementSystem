using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class TestSubmission
    {
        public Test Test { get; }

        public CourseMember Student { get; }

        public ICollection<string> Answers { get; }

        public TestSubmission(Test test, ICollection<string> answers)
        {
            Test = test;
            Answers = answers;
        }
    }
}
