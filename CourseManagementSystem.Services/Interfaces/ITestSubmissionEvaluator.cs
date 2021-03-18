using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Services.Interfaces
{
    /// <summary>
    /// evaluator of test submissions
    /// </summary>
    public interface ITestSubmissionEvaluator
    {
        /// <summary>
        /// evaluate the given test submission - set points to <see cref="TestSubmission.Answers"/>
        /// </summary>
        /// <param name="testSubmission">submission to evaluate</param>
        void Evaluate(TestSubmission testSubmission);
    }
}
