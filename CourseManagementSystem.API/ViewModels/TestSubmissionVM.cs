using CourseManagementSystem.API.Validation.Attributes;
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

        protected BaseTestSubmissionVM(string testSubmissionId, DateTime submittedDateTime, bool isReviewed)
        {
            TestSubmissionId = testSubmissionId;
            SubmittedDateTime = submittedDateTime;
            IsReviewed = isReviewed;
        }

        /// <summary>
        /// id of the test submission
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string TestSubmissionId { get; set; }

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

        /// <summary>
        /// create new viewmodel representing basic info about a test submission
        /// </summary>
        /// <param name="testSubmissionId">id of the test submission</param>
        /// <param name="testTopic">topic of the test</param>
        /// <param name="testWeight">weight of the test</param>
        /// <param name="percentualScore">percentual score of the submission</param>
        /// <param name="submittedDateTime">exact date and time when was this submission submitted</param>
        /// <param name="isReviewed">is this submission marked as reviewed?</param>
        public TestSubmissionInfoVM(string testSubmissionId, string testTopic, int testWeight, double percentualScore, DateTime submittedDateTime, bool isReviewed)
            : base(testSubmissionId, submittedDateTime, isReviewed)
        {
            TestTopic = testTopic;
            TestWeight = testWeight;
            PercentualScore = percentualScore;
        }

        /// <summary>
        /// topic of the test
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string TestTopic { get; set; }

        /// <summary>
        /// weight of the test
        /// </summary>
        [PositiveIntValue]
        public int TestWeight { get; set; }

        /// <summary>
        /// score from the test (in percents)
        /// </summary>
        [NonNegativeDoubleValue]
        public double PercentualScore { get; set; }
    }

    /// <summary>
    /// class representing info about test submission and course memeber
    /// </summary>
    public class TestSubmissionWithUserInfoVM : BaseTestSubmissionVM
    {
        public TestSubmissionWithUserInfoVM() : base()
        { }

        /// <summary>
        /// create new viewmodel representing info about test submission and course member
        /// </summary>
        /// <param name="studentEmail">email of the student</param>
        /// <param name="testSubmissionId">id of the test submission</param>
        /// <param name="percentualScore">score of the submission</param>
        /// <param name="submittedDateTime">exact date and time when was this submission submitted</param>
        /// <param name="isReviewed">is this submission marked as reviewed?</param>
        public TestSubmissionWithUserInfoVM(string studentEmail, string testSubmissionId, double percentualScore, DateTime submittedDateTime, bool isReviewed)
            : base(testSubmissionId, submittedDateTime, isReviewed)
        {
            StudentEmail = studentEmail;
            PercentualScore = percentualScore;
        }

        /// <summary>
        /// email of the student
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string StudentEmail { get; set; }

        /// <summary>
        /// percentual score gained from the test
        /// </summary>
        [NonNegativeDoubleValue]
        public double PercentualScore { get; set; }
    }

    /// <summary>
    /// this class represents viewmodel for test with submission
    /// </summary>
    public class TestWithSubmissionVM : BaseTestSubmissionVM
    {
        public TestWithSubmissionVM() : base()
        { }

        /// <summary>
        /// create new viewmodel representing test with the submission
        /// </summary>
        /// <param name="testId">id of the test</param>
        /// <param name="testTopic">topic of the test</param>
        /// <param name="testSubmissionId">id of the test submission</param>
        /// <param name="answers">submitted answers</param>
        /// <param name="submittedDateTime">exact date and time of the submission</param>
        /// <param name="isReviewed">is this submission marked as reviewed?</param>
        /// <param name="isTestGraded">is this test graded?</param>
        public TestWithSubmissionVM(string testId, string testTopic, string testSubmissionId, IEnumerable<SubmissionAnswerWithCorrectAnswerVM> answers,
            DateTime submittedDateTime, bool isReviewed, bool isTestGraded) : base(testSubmissionId, submittedDateTime, isReviewed)
        {
            TestId = testId;
            TestTopic = testTopic;
            Answers = answers;
            IsTestGraded = isTestGraded;
        }

        /// <summary>
        /// id of the test
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string TestId { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string TestTopic { get; set; }

        /// <summary>
        /// is this test graded?
        /// </summary>
        public bool IsTestGraded { get; set; }

        /// <summary>
        /// submitted and correct answers
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public IEnumerable<SubmissionAnswerWithCorrectAnswerVM> Answers { get; set; }
    }
}