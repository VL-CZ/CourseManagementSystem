using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CourseManagementSystem.TestEvaluation.Tests
{
    public class TestSubmissionEvaluatorTests
    {
        private List<TestQuestion> questions;
        private TestSubmission submissionToEvaluate;
        private TestSubmissionEvaluator testSubmissionEvaluator;

        public TestSubmissionEvaluatorTests()
        {
            testSubmissionEvaluator = new TestSubmissionEvaluator();

            questions = new List<TestQuestion>()
            {
                new TestQuestion(1, "What's the capital city of Russia?", "Moscow", 3),
                new TestQuestion(1, "What's the capital city of Japan?", "Tokyo", 2),
                new TestQuestion(1, "What's the capital city of the UK?", "London", 1),
            };
            
            var courseTest = new CourseTest(string.Empty, questions, 0, DateTime.Now, true);
            submissionToEvaluate = new TestSubmission(courseTest, null, new List<TestSubmissionAnswer>());
        }

        /// <summary>
        /// test Evaluate method on test submission that has both correct and wrong answers
        /// </summary>
        [Fact]
        public void Evaluate_CorrectAndWrong()
        {
            submissionToEvaluate.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(questions[0], "Moscow"),
                new TestSubmissionAnswer(questions[1], "Prague"),
                new TestSubmissionAnswer(questions[2], "London"),
            };

            testSubmissionEvaluator.Evaluate(submissionToEvaluate);

            var answers = submissionToEvaluate.Answers.ToList();
            
            Assert.Equal(3, answers[0].Points);
            Assert.Equal(0, answers[1].Points);
            Assert.Equal(1, answers[2].Points);
        }

        /// <summary>
        /// test Evaluate method on test submission that has only corrent answers
        /// </summary>
        [Fact]
        public void Evaluate_AllCorrect()
        {
            submissionToEvaluate.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(questions[0], "Moscow"),
                new TestSubmissionAnswer(questions[1], "Tokyo"),
                new TestSubmissionAnswer(questions[2], "London"),
            };

            testSubmissionEvaluator.Evaluate(submissionToEvaluate);

            var answers = submissionToEvaluate.Answers.ToList();

            Assert.Equal(3, answers[0].Points);
            Assert.Equal(2, answers[1].Points);
            Assert.Equal(1, answers[2].Points);
        }

        /// <summary>
        /// test Evaluate method on test submission that has only wrong answers
        /// </summary>
        [Fact]
        public void Evaluate_AllWrong()
        {
            submissionToEvaluate.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(questions[0], "Stockholm"),
                new TestSubmissionAnswer(questions[1], ""),
                new TestSubmissionAnswer(questions[2], null),
            };

            testSubmissionEvaluator.Evaluate(submissionToEvaluate);

            var answers = submissionToEvaluate.Answers.ToList();

            Assert.Equal(0, answers[0].Points);
            Assert.Equal(0, answers[1].Points);
            Assert.Equal(0, answers[2].Points);
        }
    }
}