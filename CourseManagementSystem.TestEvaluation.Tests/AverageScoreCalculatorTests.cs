using CourseManagementSystem.TestEvaluation.Calculators;
using CourseManagementSystem.TestEvaluation.DTOs;
using System.Collections.Generic;
using Xunit;

namespace CourseManagementSystem.UnitTests
{
    /// <summary>
    /// tests for <see cref="AverageScoreCalculator"/> class
    /// </summary>
    public class AverageScoreCalculatorTests
    {
        private IList<ScoreWithWeightDto> weightedScores;
        private readonly int decimalDigitPrecision = 5;

        public AverageScoreCalculatorTests()
        {
            weightedScores = new List<ScoreWithWeightDto>();
        }

        /// <summary>
        /// test simple case where all the tests have weight 1
        /// </summary>
        [Fact]
        public void CalculateScore_Simple()
        {
            weightedScores = new List<ScoreWithWeightDto>
            {
                new ScoreWithWeightDto(1,1),
                new ScoreWithWeightDto(1,0),
                new ScoreWithWeightDto(1,0.5)
            };
            double expectedTotalScore = 0.5;

            double totalScore = AverageScoreCalculator.GetScore(weightedScores);

            Assert.Equal(expectedTotalScore, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test more complex case (different weights and scores)
        /// </summary>
        [Fact]
        public void GetScore_Complex()
        {
            weightedScores = new List<ScoreWithWeightDto>
            {
                new ScoreWithWeightDto(1,1),
                new ScoreWithWeightDto(10,0.7),
                new ScoreWithWeightDto(3,0.85),
                new ScoreWithWeightDto(5,1),
                new ScoreWithWeightDto(1,0.4),
            };
            double expectedTotalScore = (1 + 7 + 2.55 + 5 + 0.4) / (1 + 10 + 3 + 5 + 1);

            double totalScore = AverageScoreCalculator.GetScore(weightedScores);

            Assert.Equal(expectedTotalScore, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test average score calculation in case that there are no scores
        /// </summary>
        [Fact]
        public void CalculateScore_NoScores()
        {
            double totalScore = AverageScoreCalculator.GetScore(new List<ScoreWithWeightDto>());
            Assert.Equal(0, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test case with a single score
        /// </summary>
        [Fact]
        public void CalculateScore_SingleScore()
        {
            weightedScores = new List<ScoreWithWeightDto>
            {
                new ScoreWithWeightDto(4,0.75)
            };

            double totalScore = AverageScoreCalculator.GetScore(weightedScores);
            Assert.Equal(0.75, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test case with a single test of big weight and many tests of small weight
        /// </summary>
        [Fact]
        public void CalculateScore_OneTestOfBigWeight()
        {
            weightedScores = new List<ScoreWithWeightDto>
            {
                new ScoreWithWeightDto(10,1)
            };

            for (int i = 0; i < 10; i++)
            {
                weightedScores.Add(new ScoreWithWeightDto(1, 0));
            }

            double totalScore = AverageScoreCalculator.GetScore(weightedScores);
            Assert.Equal(0.5, totalScore, decimalDigitPrecision);
        }

        /// <summary>
        /// test case with an average score over 100%
        /// </summary>
        [Fact]
        public void CalculateScore_Over100Percent()
        {
            weightedScores = new List<ScoreWithWeightDto>
            {
                new ScoreWithWeightDto(5,1.1),
                new ScoreWithWeightDto(5,1)
            };

            double totalScore = AverageScoreCalculator.GetScore(weightedScores);
            Assert.Equal(1.05, totalScore, decimalDigitPrecision);
        }
    }
}