using CourseManagementSystem.API.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public SubmitTestVM(string testId, string testTopic, IEnumerable<SubmissionAnswerVM> answers)
        {
            TestId = testId;
            TestTopic = testTopic;
            Answers = answers;
        }

        /// <summary>
        /// id of the test
        /// </summary>
        [Required(ErrorMessage = ValidationConfig.requiredFieldErrorMessage)]
        public string TestId { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        [Required(ErrorMessage = ValidationConfig.requiredFieldErrorMessage)]
        public string TestTopic { get; set; }

        /// <summary>
        /// answers submitted by student
        /// </summary>
        [Required(ErrorMessage = ValidationConfig.requiredFieldErrorMessage)]
        public IEnumerable<SubmissionAnswerVM> Answers { get; set; }
    }
}