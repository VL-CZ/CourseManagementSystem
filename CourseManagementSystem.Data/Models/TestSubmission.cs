using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class TestSubmission
    {
        public CourseTest Test { get; }

        public CourseMember Student { get; }

        public ICollection<Answer> Answers { get; }

        public TestSubmission(CourseTest test, ICollection<Answer> answers)
        {
            Test = test;
            Answers = answers;
        }

        public int GetPoints()
        {
            int points = 0;

            foreach (var answer in Answers)
            {
                var question = Test.Questions.Where(q => q.Number == answer.QuestionNumber).Single();
                if (question.CorrectAnswer == answer.Text)
                {
                    points++;
                }
            }
            return points;
        }
    }
}
