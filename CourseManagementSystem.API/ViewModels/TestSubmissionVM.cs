using System;
using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    public abstract class BaseTestInfoVM
    {

    }

    /// <summary>
    /// class representing basic info about a test submission
    /// </summary>
    public class TestSubmissionSummaryVM
    {
        public TestSubmissionSummaryVM()
        { }

        public TestSubmissionSummaryVM(int testSubmissionId, string testTopic, int testWeight, double percentualScore)
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

        /// <summary>
        /// datetime when this test was submitted
        /// </summary>
        public DateTime SubmittedDateTime { get; set; }

        /// <summary>
        /// has this test already been reviewed?
        /// </summary>
        public bool IsReviewed { get; set; }
    }

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

        /// <summary>
        /// datetime when this test was submitted
        /// </summary>
        public DateTime SubmittedDateTime { get; set; }

        /// <summary>
        /// has this test already been reviewed?
        /// </summary>
        public bool IsReviewed { get; set; }
    }

    /// <summary>
    /// this class represents viewmodel for test with submission
    /// </summary>
    public class TestWithSubmissionVM
    {
        public TestWithSubmissionVM() { }

        public TestWithSubmissionVM(int testId, string testTopic, int submissionId, IEnumerable<SubmissionAnswerWithCorrectAnswerVM> answers)
        {
            TestId = testId;
            TestTopic = testTopic;
            Answers = answers;
            SubmissionId = submissionId;
        }

        /// <summary>
        /// id of the test
        /// </summary>
        public int TestId { get; }

        /// <summary>
        /// topic of the test
        /// </summary>
        public string TestTopic { get; }

        /// <summary>
        /// id of the test submission
        /// </summary>
        public int SubmissionId { get; }

        /// <summary>
        /// datetime when this test was submitted
        /// </summary>
        public DateTime SubmittedDateTime { get; set; }

        /// <summary>
        /// has this test already been reviewed?
        /// </summary>
        public bool IsReviewed { get; set; }

        /// <summary>
        /// submitted and correct answers
        /// </summary>
        public IEnumerable<SubmissionAnswerWithCorrectAnswerVM> Answers { get; }
    }
}