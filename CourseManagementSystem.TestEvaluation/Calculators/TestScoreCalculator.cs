using CourseManagementSystem.Data.Models;
using System.Linq;

namespace CourseManagementSystem.TestEvaluation.Calculators
{
    /// <summary>
    /// class responsible for calculating test score
    /// </summary>
    public static class TestScoreCalculator
    {
        /// <summary>
        /// calculate percentual score of the given test submission (0 = 0%, 1 = 100%, can be greater than 1 in case of bonus points)
        /// </summary>
        /// <param name="testSubmission">the given submission</param>
        /// <returns>score of the test (0 = 0%, 1 = 100%, can be greater than 1 in case of bonus points)</returns>
        public static double CalculateScore(TestSubmission testSubmission)
        {
            int maximalScore = testSubmission.Answers.Sum(answer => answer.Question.Points);
            int studentScore = testSubmission.Answers.Sum(answer => answer.Points);

            return (double)studentScore / maximalScore;
        }
    }
}