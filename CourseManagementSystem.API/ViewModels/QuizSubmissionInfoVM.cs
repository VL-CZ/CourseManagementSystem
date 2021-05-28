using CourseManagementSystem.API.Validation.Attributes;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel for quiz submission
    /// </summary>
    public class QuizSubmissionInfoVM
    {
        public QuizSubmissionInfoVM()
        { }

        public QuizSubmissionInfoVM(string testSubmissionId, string testTopic)
        {
            TestSubmissionId = testSubmissionId;
            TestTopic = testTopic;
        }

        /// <summary>
        /// identifier of the <see cref="Data.Models.TestSubmission"/>
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string TestSubmissionId { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string TestTopic { get; set; }
    }
}