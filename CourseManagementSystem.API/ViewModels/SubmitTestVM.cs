using CourseManagementSystem.API.Validation.Attributes;
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

        public SubmitTestVM(string testId, string testTopic, bool isSubmitted, IEnumerable<SubmissionAnswerVM> answers, bool isTestGraded)
        {
            TestSubmissionId = testId;
            TestTopic = testTopic;
            Answers = answers;
            IsSubmitted = isSubmitted;
            IsTestGraded = isTestGraded;
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
        /// answers submitted by student
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public IEnumerable<SubmissionAnswerVM> Answers { get; set; }
    }
}