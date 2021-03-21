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
            return dbContext.TestSubmissions.Include(ts => ts.Test).Include(ts => ts.Student).Include(ts => ts.Answers).ThenInclude(ans => ans.Question)
                .Where(ts => ts.Student.Id == courseMemberId);
        }

        /// <inheritdoc/>
        public TestSubmission GetSubmissionById(int testSubmissionId)
        {
            return dbContext.TestSubmissions.Include(ts => ts.Answers).ThenInclude(a => a.Question).Include(ts => ts.Test).SingleOrDefault(ts => ts.Id == testSubmissionId);
        }
    }
}
