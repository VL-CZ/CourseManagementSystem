using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel for test submission
    /// </summary>
    public class TestSubmissionVM
    {
        public TestSubmissionVM()
        {
        }

        public TestSubmissionVM(int testId, string testTopic, IEnumerable<SubmissionAnswerVM> answers)
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

    /// <summary>
    /// class representing the submission answers submitted by students
    /// </summary>
    public class SubmissionAnswerVM
    {
        public SubmissionAnswerVM()
        {
        }

        public SubmissionAnswerVM(int questionNumber, string questionText, string answerText)
        {
            QuestionNumber = questionNumber;
            QuestionText = questionText;
            AnswerText = answerText;
        }

        /// <summary>
        /// number of question that this answer belongs to
        /// </summary>
        public int QuestionNumber { get; set; }

        /// <summary>
        /// text of the question
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// answer submitted by the student
        /// </summary>
        public string AnswerText { get; set; }
    }
}