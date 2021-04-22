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

        public DbSet<TestSubmissionAnswer> TestSubmissionAnswers { get; set; }

        public DbSet<ForumPost> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureForeignKeys(builder);
        }

        /// <summary>
        /// configure foreign keys of the database
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureForeignKeys(ModelBuilder builder)
        {
            // Course

            builder.Entity<Course>()
                .HasOne(course => course.Admin)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // CourseFile

            builder.Entity<CourseFile>()
                .HasOne(file => file.Course)
                .WithMany(course => course.Files)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseMember

            builder.Entity<CourseMember>()
                .HasOne(cm => cm.User)
                .WithMany(person => person.CourseMemberships)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CourseMember>()
                .HasOne(cm => cm.Course)
                .WithMany(course => course.Members)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseTest

            builder.Entity<CourseTest>()
                .HasOne(test => test.Course)
                .WithMany(course => course.Tests)
                .OnDelete(DeleteBehavior.Restrict);

            // ForumPost

            builder.Entity<ForumPost>()
                .HasOne(post => post.Course)
                .WithMany(course => course.ForumPosts)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ForumPost>()
                .HasOne(post => post.Author)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Grade

            builder.Entity<Grade>()
                .HasOne(grade => grade.Student)
                .WithMany(cm => cm.Grades)
                .OnDelete(DeleteBehavior.Restrict);

            // TestQuestion

            builder.Entity<TestQuestion>()
                .HasOne<CourseTest>()
                .WithMany(test => test.Questions)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // TestSubmission

            builder.Entity<TestSubmission>()
                .HasOne(ts => ts.Test)
                .WithMany(test => test.Submissions)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TestSubmission>()
                .HasOne(ts => ts.Student)
                .WithMany(cm => cm.TestSubmissions)
                .OnDelete(DeleteBehavior.Restrict);

            // TestSubmissionAnswer

            builder.Entity<TestSubmissionAnswer>()
                .HasOne(answer => answer.Question)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
