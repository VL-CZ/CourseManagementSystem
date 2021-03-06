using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.ViewModels
{
    public class TestSubmissionVM
    {
        public int TestId { get; set; }

        public IEnumerable<SubmissionAnswerVM> Answers { get; set; }

    }

    public class SubmissionAnswerVM
    {
        public int QuestionNumber { get; set; }

        public string Text { get; set; }
    }
}
