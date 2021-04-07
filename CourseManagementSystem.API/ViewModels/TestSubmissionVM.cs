using System;
using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// base viewmodel for a test submission
    /// </summary>
    public abstract class BaseTestSubmissionVM
    {
        protected BaseTestSubmissionVM()
        { }

        protected BaseTestSubmissionVM(int testSubmissionId, DateTime submittedDateTime, bool isReviewed)
        {
            TestSubmissionId = testSubmissionId;
            SubmittedDateTime = submittedDateTime;
            IsReviewed = isReviewed;
        }

        /// <summary>
        /// id of the test submission
        /// </summary>
        public int TestSubmissionId { get; set; }

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
    /// class representing basic info about a test submission
    /// </summary>
    public class TestSubmissionInfoVM : BaseTestSubmissionVM
    {
        public TestSubmissionInfoVM() : base()
        { }

        public TestSubmissionInfoVM(int testSubmissionId, string testTopic, int testWeight, double percentualScore, DateTime submittedDateTime, bool isReviewed)
            : base(testSubmissionId, submittedDateTime, isReviewed)
        {
            TestTopic = testTopic;
            TestWeight = testWeight;
            PercentualScore = percentualScore;
        }

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

    /// <summary>
    /// class representing info about test submission and course memeber
    /// </summary>
    public class TestSubmissionWithUserInfoVM : BaseTestSubmissionVM
    {
        public TestSubmissionWithUserInfoVM() : base()
        { }

        public TestSubmissionWithUserInfoVM(string studentEmail, int testSubmissionId, double percentualScore, DateTime submittedDateTime, bool isReviewed)
            : base(testSubmissionId, submittedDateTime, isReviewed)
        {
            StudentEmail = studentEmail;
            PercentualScore = percentualScore;
        }

        /// <summary>
        /// email of the student
        /// </summary>
        public string StudentEmail { get; set; }

        /// <summary>
        /// percentual score gained from the test
        /// </summary>
        public double PercentualScore { get; set; }
    }

    /// <summary>
    /// this class represents viewmodel for test with submission
    /// </summary>
    public class TestWithSubmissionVM : BaseTestSubmissionVM
    {
        public TestWithSubmissionVM() : base()
        { }

        public TestWithSubmissionVM(int testId, string testTopic, int testSubmissionId, IEnumerable<SubmissionAnswerWithCorrectAnswerVM> answers,
            DateTime submittedDateTime, bool isReviewed) : base(testSubmissionId, submittedDateTime, isReviewed)
        {
            TestId = testId;
            TestTopic = testTopic;
            Answers = answers;
        }

        /// <summary>
        /// id of the test
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        public string TestTopic { get; set; }

        /// <summary>
        /// submitted and correct answers
        /// </summary>
        public IEnumerable<SubmissionAnswerWithCorrectAnswerVM> Answers { get; set; }
    }
}