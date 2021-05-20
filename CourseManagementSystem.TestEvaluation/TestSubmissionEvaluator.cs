using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.TestEvaluation
{
    /// <summary>
    /// evaluator of test submissions
    /// </summary>
    public class TestSubmissionEvaluator
    {
        /// <summary>
        /// evaluate the given test submission - set points to <see cref="TestSubmission.Answers"/>
        /// </summary>
        /// <param name="testSubmission">submission to evaluate</param>
        public void Evaluate(TestSubmission testSubmission)
        {
            foreach (TestSubmissionAnswer answer in testSubmission.Answers)
            {
                answer.Points = (answer.Text == answer.Question.CorrectAnswer) ? answer.Question.Points : 0;
            }
        }
    }
}