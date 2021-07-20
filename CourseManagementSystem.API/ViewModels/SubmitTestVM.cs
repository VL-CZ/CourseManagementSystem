using CourseManagementSystem.API.Validation.Attributes;
using System;
using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel for submitting a test
    /// </summary>
    public class SubmitTestVM
    {
        public SubmitTestVM()
        {
        }

        /// <summary>
        /// create new viewmodel for submitting a test
        /// </summary>
        /// <param name="testId">id of the test</param>
        /// <param name="testTopic">topic of the test</param>
        /// <param name="isSubmitted">is this submission marked as submitted?</param>
        /// <param name="answers">submitted answers</param>
        /// <param name="isTestGraded">is this test graded?</param>
        /// <param name="testDeadline">deadline of the test</param>
        public SubmitTestVM(string testId, string testTopic, bool isSubmitted, IEnumerable<SubmissionAnswerVM> answers, bool isTestGraded, DateTime testDeadline)
        {
            TestSubmissionId = testId;
            TestTopic = testTopic;
            Answers = answers;
            IsSubmitted = isSubmitted;
            IsTestGraded = isTestGraded;
            TestDeadline = testDeadline;
        }

        /// <summary>
        /// id of the test submission
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string TestSubmissionId { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string TestTopic { get; set; }

        /// <summary>
        /// check if the test has already been submitted
        /// </summary>
        public bool IsSubmitted { get; set; }

        /// <summary>
        /// is the test graded?
        /// </summary>
        public bool IsTestGraded { get; set; }

        /// <summary>
        /// deadline of the test
        /// </summary>
        public DateTime TestDeadline { get; set; }

        /// <summary>
        /// answers submitted by student
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public IEnumerable<SubmissionAnswerVM> Answers { get; set; }
    }
}