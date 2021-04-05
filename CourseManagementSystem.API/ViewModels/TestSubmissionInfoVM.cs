﻿namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// class representing basic info about the test submission
    /// </summary>
    public class TestSubmissionInfoVM
    {
        public TestSubmissionInfoVM()
        { }

        public TestSubmissionInfoVM(int testSubmissionId, string testTopic, int testWeight, double percentualScore)
        {
            TestSubmissionId = testSubmissionId;
            TestTopic = testTopic;
            TestWeight = testWeight;
            PercentualScore = percentualScore;
        }

        /// <summary>
        /// id of the test submission
        /// </summary>
        public int TestSubmissionId { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        public string TestTopic { get; set; }

        /// <summary>
        /// weight of the test
        /// </summary>
        public int TestWeight { get; set; }

        /// <summary>
        /// score from the test (in percents)
        /// </summary>
        public double PercentualScore { get; set; }
    }
}