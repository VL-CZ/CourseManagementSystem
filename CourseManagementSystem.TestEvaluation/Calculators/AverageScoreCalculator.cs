using CourseManagementSystem.TestEvaluation.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.TestEvaluation.Calculators
{
    /// <summary>
    /// class responsible for calculating overall score for the submissions
    /// </summary>
    public class AverageScoreCalculator
    {
        /// <summary>
        /// calculate average score
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        public static double GetScore(IEnumerable<ScoreWithWeightDto> scores)
        {
            // no given scores -> return 0
            if (!scores.Any())
            {
                return 0;
            }

            int weightSum = scores.Sum(testScore => testScore.Weight);
            double totalScoreSum = scores.Sum(testScore => testScore.Score * testScore.Weight);

            return totalScoreSum / weightSum;
        }
    }
}