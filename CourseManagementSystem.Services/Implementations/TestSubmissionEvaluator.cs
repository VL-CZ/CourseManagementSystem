using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;

namespace CourseManagementSystem.Services.Implementations
{
    public class TestSubmissionEvaluator : ITestSubmissionEvaluator
    {
        public void Evaluate(TestSubmission testSubmission)
        {
            foreach (TestSubmissionAnswer answer in testSubmission.Answers)
            {
                answer.Points = (answer.Text == answer.Question.CorrectAnswer) ? answer.Question.Points : 0;
            }
        }
    }
}
