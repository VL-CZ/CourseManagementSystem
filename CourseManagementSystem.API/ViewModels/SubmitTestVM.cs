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

        public SubmitTestVM(int testId, string testTopic, IEnumerable<SubmissionAnswerVM> answers)
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
        /// answers submitted by student
        /// </summary>
        public IEnumerable<SubmissionAnswerVM> Answers { get; set; }
    }
}