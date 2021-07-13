using CourseManagementSystem.Data.Models;
using CourseManagementSystem.TestEvaluation.Calculators;
using System.Collections.Generic;
using Xunit;

namespace CourseManagementSystem.TestEvaluation.Tests
{
    /// <summary>
    /// tests for <see cref="TestScoreCalculator"/> class
    /// </summary>
    public class TestScoreCalculatorTests
    {
        private TestSubmission testSubmission;
        private IList<TestQuestion> availableQuestions;
        private readonly int decimalDigitPrecision = 5;

        public TestScoreCalculatorTests()
        {
            testSubmission = new TestSubmission();

            availableQuestions = new List<TestQuestion>
            {
                new TestQuestion(0,"Q0","China",10,Data.QuestionType.TextAnswer),
                new TestQuestion(1,"Q1","A1",1,Data.QuestionType.TextAnswer),
                new TestQuestion(2,"Q2","2",5,Data.QuestionType.TextAnswer),
                new TestQuestion(3,"Q3","A3",2,Data.QuestionType.TextAnswer),
                new TestQuestion(4,"Q4","A4",3,Data.QuestionType.TextAnswer),
                new TestQuestion(5,"Q5","A5",1,Data.QuestionType.TextAnswer),
                new TestQuestion(6,"Q6","yes",4,Data.QuestionType.TextAnswer),
                new TestQuestion(7,"Q7","no",1,Data.QuestionType.TextAnswer),
            };
        }

        /// <summary>
        /// test classical score calculation
        /// </summary>
        [Fact]
        public void CalculateScore_Normal()
        {
            testSubmission.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(){Question=availableQuestions[1],Points=1},
                new TestSubmissionAnswer(){Question=availableQuestions[3],Points=1},
                new TestSubmissionAnswer(){Question=availableQuestions[5],Points=0},
                new TestSubmissionAnswer(){Question=availableQuestions[6],Points=4},
            };

            double score = TestScoreCalculator.CalculateScore(testSubmission);

            Assert.Equal(0.75, score, decimalDigitPrecision);
        }

        /// <summary>
        /// test score calculation on the test with single question
        /// </summary>
        [Fact]
        public void CalculateScore_SingleQuestion()
        {
            testSubmission.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(){Question=availableQuestions[1],Points=1}
            };

            double score = TestScoreCalculator.CalculateScore(testSubmission);

            Assert.Equal(1, score, decimalDigitPrecision);
        }

        /// <summary>
        /// test score calculation where the score is over 100%
        /// </summary>
        [Fact]
        public void CalculateScore_Over100Percent()
        {
            testSubmission.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(){Question=availableQuestions[2],Points=6},
                new TestSubmissionAnswer(){Question=availableQuestions[6],Points=4},
                new TestSubmissionAnswer(){Question=availableQuestions[7],Points=1}
            };

            double score = TestScoreCalculator.CalculateScore(testSubmission);
            Assert.Equal(1.1, score, decimalDigitPrecision);
        }

        /// <summary>
        /// test score calculation where the score is 0%
        /// </summary>
        [Fact]
        public void CalculateScore_ZeroPoints()
        {
            testSubmission.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(){Question=availableQuestions[1],Points=0},
                new TestSubmissionAnswer(){Question=availableQuestions[2],Points=0},
                new TestSubmissionAnswer(){Question=availableQuestions[4],Points=0},
                new TestSubmissionAnswer(){Question=availableQuestions[6],Points=0}
            };

            double score = TestScoreCalculator.CalculateScore(testSubmission);
            Assert.Equal(0, score, decimalDigitPrecision);
        }
    }
}