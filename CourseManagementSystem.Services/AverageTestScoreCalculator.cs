using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Services
{
    /// <summary>
    /// class responsible for calculating overall score for the submissions
    /// </summary>
    public class AverageTestScoreCalculator
    {
        /// <summary>
        /// calculate average score
        /// </summary>
        /// <param name="testScores"></param>
        /// <returns></returns>
        public static double GetScore(IEnumerable<TestSubmissionScoreDto> testScores)
        {
            // no given scores -> return 0
            if (!testScores.Any())
            {
                return 0;
            }

            int weightSum = testScores.Sum(testScore => testScore.TestWeight);
            double totalScoreSum = testScores.Sum(testScore => testScore.Score * testScore.TestWeight);

            return totalScoreSum / weightSum;
        }
    }
}