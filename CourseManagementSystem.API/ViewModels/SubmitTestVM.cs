﻿using CourseManagementSystem.API.Validation.Attributes;
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

        public SubmitTestVM(string testId, string testTopic, IEnumerable<SubmissionAnswerVM> answers)
        {
            TestId = testId;
            TestTopic = testTopic;
            Answers = answers;
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
        /// answers submitted by student
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public IEnumerable<SubmissionAnswerVM> Answers { get; set; }
    }
}