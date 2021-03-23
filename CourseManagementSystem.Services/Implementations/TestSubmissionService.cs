using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseManagementSystem.Services.Implementations
{
    public class TestSubmissionService : ITestSubmissionService
    {
        private readonly CMSDbContext dbContext;

        public TestSubmissionService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public IEnumerable<TestSubmission> GetAllSubmissionsOfCourseMember(int courseMemberId)
        {
            return GetTestSubmissionsWithTestAndStudent().Where(ts => ts.Student.Id == courseMemberId);
        }

        /// <inheritdoc/>
        public IEnumerable<TestSubmission> GetAllSubmissionsOfTest(int testId)
        {
            return GetTestSubmissionsWithTestAndStudent().Where(ts => ts.Test.Id == testId);
        }

        /// <inheritdoc/>
        public TestSubmissionAnswer GetAnswerByQuestionNumber(TestSubmission testSubmission, int questionNumber)
        {
            return testSubmission.Answers.SingleOrDefault(answer => answer.Question.Number == questionNumber);
        }

        /// <inheritdoc/>
        public TestSubmission GetSubmissionById(int testSubmissionId)
        {
            return dbContext.TestSubmissions.Include(ts => ts.Answers).ThenInclude(a => a.Question).Include(ts => ts.Test).SingleOrDefault(ts => ts.Id == testSubmissionId);
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
