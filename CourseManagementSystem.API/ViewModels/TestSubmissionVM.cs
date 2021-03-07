using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.ViewModels
{
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

        public int TestId { get; set; }

        public string TestTopic { get; set; }

        public IEnumerable<SubmissionAnswerVM> Answers { get; set; }

    }

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

        public int QuestionNumber { get; set; }

        public string QuestionText { get; set; }

        public string AnswerText { get; set; }
    }
}
