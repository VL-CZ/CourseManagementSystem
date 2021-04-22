namespace CourseManagementSystem.TestEvaluation.DTOs
{
    /// <summary>
    /// DTO for test submission weight and score
    /// </summary>
    public class TestSubmissionScoreDto
    {
        public TestSubmissionScoreDto(int testWeight, double score)
        {
            TestWeight = testWeight;
            Score = score;
        }

        /// <summary>
        /// weight of the test (e.g. impact on overall grade)
        /// </summary>
        public int TestWeight { get; }

        /// <summary>
        /// score recieved from the test (0=0%, 1=100%, can be greater than 1 in case of bonus points)
        /// </summary>
        public double Score { get; }
    }
}