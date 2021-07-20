using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class CourseService : DbService, ICourseService
    {
        public CourseService(CMSDbContext dbContext) : base(dbContext)
        { }

        /// <inheritdoc/>
        public void ArchiveById(string courseId)
        {
            Course c = GetById(courseId);
            c.IsArchived = true;
        }

        /// <inheritdoc/>
        public void AddAdmin(Person admin, string courseId)
        {
            var course = GetById(courseId);
            var courseAdmin = new CourseAdmin(admin, course);
            dbContext.CourseAdmins.Add(courseAdmin);
        }

        /// <inheritdoc/>
        public void RequestEnrollment(Person person, string courseId)
        {
            var course = GetById(courseId);
            var newEnrollmentRequest = new EnrollmentRequest(course, person);
            dbContext.EnrollmentRequests.Add(newEnrollmentRequest);
        }

        /// <inheritdoc/>
        public ICollection<CourseAdmin> GetAdminsWithUsers(string courseId)
        {
            return dbContext.Courses
                .Include(course => course.Admins)
                .ThenInclude(member => member.User)
                .Single(course => course.Id.ToString() == courseId)
                .Admins;
        }

        /// <inheritdoc/>
        public Course GetById(string courseId)
        {
            return dbContext.Courses.FindById(courseId);
        }

        /// <inheritdoc/>
        public void AddCourse(Course course)
        {
            dbContext.Courses.Add(course);
        }

        /// <inheritdoc/>
        public ICollection<CourseFile> GetFiles(string courseId)
        {
            return dbContext.Courses
                .Include(course => course.Files)
                .Single(course => course.Id.ToString() == courseId)
                .Files;
        }

        /// <inheritdoc/>
        public ICollection<CourseTest> GetTests(string courseId)
        {
            return dbContext.Courses
                .Include(course => course.Tests)
                .Single(course => course.Id.ToString() == courseId)
                .Tests;
        }

        /// <inheritdoc/>
        public ICollection<ForumPost> GetPostsWithAuthors(string courseId)
        {
            return dbContext.Courses
                .Include(course => course.ForumPosts)
                .ThenInclude(post => post.Author)
                .Single(course => course.Id.ToString() == courseId)
                .ForumPosts;
        }

        /// <inheritdoc/>
        public ICollection<CourseMember> GetMembersWithUsers(string courseId)
        {
            return dbContext.Courses
                .Include(course => course.Members)
                .ThenInclude(member => member.User)
                .Single(course => course.Id.ToString() == courseId)
                .Members.Where(cm => !cm.IsArchived)
                .ToList();
        }

        /// <inheritdoc/>
        public ICollection<EnrollmentRequest> GetEnrollmentRequestsWithPeople(string courseId)
        {
            return dbContext.Courses
                .Include(course => course.EnrollmentRequests)
                .ThenInclude(er => er.Person)
                .Single(course => course.Id.ToString() == courseId)
                .EnrollmentRequests;
        }
    }
}