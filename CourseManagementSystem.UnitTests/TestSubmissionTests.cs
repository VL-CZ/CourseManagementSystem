using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace CourseManagementSystem.UnitTests
{
    public class TestSubmissionTests
    {
        [Fact]
        public void CheckPoints()
        {
            var questions = new List<TestQuestion>()
            {
                new TestQuestion(1,"Q1","A1"),
                new TestQuestion(3,"Q3","A3"),
                new TestQuestion(2,"Q1","A2")
            };
            var test = new CourseTest(questions);

            var answers = new List<Answer>()
            {
                new Answer(1,"A1"),
                new Answer(2,"I have no idea"),
                new Answer(3,"")
            };

            var testSubmission = new TestSubmission(test, answers);

            int points = testSubmission.GetPoints();

            Assert.True(1 == points, "Total points incorrect");
        }
    }
}
