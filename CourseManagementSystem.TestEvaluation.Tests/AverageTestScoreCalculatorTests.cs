using CourseManagementSystem.TestEvaluation.Calculators;
using CourseManagementSystem.TestEvaluation.DTOs;
using System.Collections.Generic;
using Xunit;

namespace CourseManagementSystem.UnitTests
{
    /// <summary>
    /// tests for <see cref="AverageTestScoreCalculator"/> class
    /// </summary>
    public class AverageTestScoreCalculatorTests
    {
        private IList<TestSubmissionScoreDto> testScores;
        private readonly int decimalDigitPrecision = 5;

        public AverageTestScoreCalculatorTests()
        {
            testScores = new List<TestSubmissionScoreDto>();
        }

        /// <summary>
        /// test simple case where all the tests have weight 1
        /// </summary>
        [Fact]
        public void CalculateScore_Simple()
        {
            testScores = new List<TestSubmissionScoreDto>
            {
                new TestSubmissionScoreDto(1,1),
                new TestSubmissionScoreDto(1,0),
                new TestSubmissionScoreDto(1,0.5)
            };
            double expectedTotalScore = 0.5;

            double totalScore = AverageTestScoreCalculator.GetScore(testScores);

            Assert.Equal(expectedTotalScore, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test more complex case (different weights and scores)
        /// </summary>
        [Fact]
        public void GetScore_Complex()
        {
            testScores = new List<TestSubmissionScoreDto>
            {
                new TestSubmissionScoreDto(1,1),
                new TestSubmissionScoreDto(10,0.7),
                new TestSubmissionScoreDto(3,0.85),
                new TestSubmissionScoreDto(5,1),
                new TestSubmissionScoreDto(1,0.4),
            };
            double expectedTotalScore = (1 + 7 + 2.55 + 5 + 0.4) / (1 + 10 + 3 + 5 + 1);

            double totalScore = AverageTestScoreCalculator.GetScore(testScores);

            Assert.Equal(expectedTotalScore, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test average score calculation in case that there are no scores
        /// </summary>
        [Fact]
        public void CalculateScore_NoScores()
        {
            double totalScore = AverageTestScoreCalculator.GetScore(new List<TestSubmissionScoreDto>());
            Assert.Equal(0, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test case with a single score
        /// </summary>
        [Fact]
        public void CalculateScore_SingleScore()
        {
            testScores = new List<TestSubmissionScoreDto>
            {
                new TestSubmissionScoreDto(4,0.75)
            };

            double totalScore = AverageTestScoreCalculator.GetScore(testScores);
            Assert.Equal(0.75, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test case with a single test of big weight and many tests of small weight
        /// </summary>
        [Fact]
        public void CalculateScore_OneTestOfBigWeight()
        {
            testScores = new List<TestSubmissionScoreDto>
            {
                new TestSubmissionScoreDto(10,1)
            };

            for (int i = 0; i < 10; i++)
            {
                testScores.Add(new TestSubmissionScoreDto(1, 0));
            }

            double totalScore = AverageTestScoreCalculator.GetScore(testScores);
            Assert.Equal(0.5, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test case with an average score over 100%
        /// </summary>
        [Fact]
        public void CalculateScore_Over100Percent()
        {
            testScores = new List<TestSubmissionScoreDto>
            {
                new TestSubmissionScoreDto(5,1.1),
                new TestSubmissionScoreDto(5,1)
            };

            double totalScore = AverageTestScoreCalculator.GetScore(testScores);
            Assert.Equal(1.05, totalScore, decimalDigitPrecision);
        }
    }
}