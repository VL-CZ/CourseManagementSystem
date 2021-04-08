using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class TestSubmissionService : DbService, ITestSubmissionService
    {
        public TestSubmissionService(CMSDbContext dbContext) : base(dbContext)
        { }

        /// <inheritdoc/>
        public IEnumerable<TestSubmission> GetAllSubmissionsOfCourseMember(string courseMemberId)
        {
            return GetTestSubmissionsWithTestAndStudent().Where(ts => ts.Student.Id.ToString() == courseMemberId);
        }

        /// <inheritdoc/>
        public IEnumerable<TestSubmission> GetAllSubmissionsOfTest(string testId)
        {
            return GetTestSubmissionsWithTestAndStudent().Where(ts => ts.Test.Id.ToString() == testId);
        }

        /// <inheritdoc/>
        public TestSubmissionAnswer GetAnswerByQuestionNumber(TestSubmission testSubmission, int questionNumber)
        {
            return testSubmission.Answers.SingleOrDefault(answer => answer.Question.Number == questionNumber);
        }

        /// <inheritdoc/>
        public TestSubmission GetSubmissionById(string testSubmissionId)
        {
            return dbContext.TestSubmissions.Include(ts => ts.Answers).ThenInclude(a => a.Question).Include(ts => ts.Test)
                .SingleOrDefault(ts => ts.Id.ToString() == testSubmissionId);
        }

        /// <inheritdoc/>
        public void MarkAsReviewed(TestSubmission testSubmission)
        {
            testSubmission.IsReviewed = true;
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void Save(TestSubmission testSubmission)
        {
            dbContext.TestSubmissions.Add(testSubmission);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void UpdateAnswer(TestSubmissionAnswer answerToEvaluate, int updatedPoints, string updatedComment)
        {
            answerToEvaluate.Points = updatedPoints;
            answerToEvaluate.Comment = updatedComment;
            dbContext.SaveChanges();
        }

        /// <summary>
        /// get all test submissions with test, student, user, answer and question loaded
        /// </summary>
        /// <returns>test submissions with test, student, user, answer and question included</returns>
        private IEnumerable<TestSubmission> GetTestSubmissionsWithTestAndStudent()
        {
            return dbContext.TestSubmissions.Include(ts => ts.Test)
                .Include(ts => ts.Student).ThenInclude(stud => stud.User)
                .Include(ts => ts.Answers).ThenInclude(ans => ans.Question);
        }
    }
}