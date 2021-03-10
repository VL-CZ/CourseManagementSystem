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
        private CMSDbContext dbContext;

        public TestSubmissionService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public IEnumerable<TestSubmission> GetAllSubmissionsOfCourseMember(int courseMemberId)
        {
            return dbContext.TestSubmissions.Include(ts => ts.Test).Include(ts => ts.Student).Where(ts => ts.Student.Id == courseMemberId);
        }
    }
}
