namespace CourseManagementSystem.TestEvaluation.DTOs
{
    /// <summary>
    /// DTO for score with its weight
    /// </summary>
    public class ScoreWithWeightDto
    {
        public ScoreWithWeightDto(int weight, double score)
        {
            Weight = weight;
            Score = score;
        }

        /// <summary>
        /// weight of score (e.g. impact on overall grade)
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// score recieved (0=0%, 1=100%, can be greater than 1 in case of bonus points)
        /// </summary>
        public double Score { get; }
    }
}