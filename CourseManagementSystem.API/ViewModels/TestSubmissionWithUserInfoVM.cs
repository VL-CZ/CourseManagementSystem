namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// class representing info about test submission and course memeber
    /// </summary>
    public class TestSubmissionWithUserInfoVM
    {
        public TestSubmissionWithUserInfoVM() { }

        public TestSubmissionWithUserInfoVM(string studentEmail, int testSubmissionId, double percentualScore)
        {
            StudentEmail = studentEmail;
            TestSubmissionId = testSubmissionId;
            PercentualScore = percentualScore;
        }

        /// <summary>
        /// email of the student
        /// </summary>
        public string StudentEmail { get; set; }

        /// <summary>
        /// id of the test submission
        /// </summary>
        public int TestSubmissionId { get; set; }

        /// <summary>
        /// percentual score gained from the test
        /// </summary>
        public double PercentualScore { get; set; }
    }
}