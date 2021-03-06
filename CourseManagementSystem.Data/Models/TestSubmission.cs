using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing one submission of the test
    /// </summary>
    public class TestSubmission
    {
        public TestSubmission()
        {
            Answers = new List<TestSubmissionAnswer>();
        }

        public TestSubmission(CourseTest test, CourseMember student, ICollection<TestSubmissionAnswer> submittedAnswers)
        {
            Test = test;
            Student = student;
            Answers = submittedAnswers;
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// test that is submitted
        /// </summary>
        public CourseTest Test { get; set; }

        /// <summary>
        /// person who submitted this <see cref="TestSubmission"/>
        /// </summary>
        public CourseMember Student { get; set; }

        /// <summary>
        /// submitted answers
        /// </summary>
        public ICollection<TestSubmissionAnswer> Answers { get; set; }

        public int GetPoints()
        {
            int points = 0;

            foreach (var answer in Answers)
            {
                if (answer.Text == answer.Question.CorrectAnswer)
                {
                    points++;
                }
            }
            return points;
        }
    }
}
