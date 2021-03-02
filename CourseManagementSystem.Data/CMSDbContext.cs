using CourseManagementSystem.Data.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace CourseManagementSystem.Data
{
    public class CMSDbContext : ApiAuthorizationDbContext<Person>
    {
        public CMSDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseMember> CourseMembers { get; set; }

        public DbSet<CourseFile> Files { get; set; }

        public DbSet<CourseTest> CourseTests { get; set; }

        public DbSet<TestQuestion> TestQuestions { get; set; }

        public DbSet<TestSubmission> TestSubmissions { get; set; }

        public DbSet<Answer> TestSubmissionAnswers { get; set; }
    }
}
