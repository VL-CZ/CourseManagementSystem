using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
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
        public void DeleteById(int courseId)
        {
            Course c = GetById(courseId);
            dbContext.Courses.Remove(c);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public Course GetById(int courseId)
        {
            return dbContext.Courses.Find(courseId);
        }


        /// <inheritdoc/>
        public void AddCourse(Course course)
        {
            dbContext.Courses.Add(course);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public ICollection<CourseFile> GetFiles(int courseId)
        {
            return dbContext.Courses.Include(c => c.Files).Single(c => c.Id == courseId).Files;
        }

        /// <inheritdoc/>
        public ICollection<CourseTest> GetTests(int courseId)
        {
            return dbContext.Courses.Include(course => course.Tests).Single(course => course.Id == courseId).Tests;
        }

        /// <inheritdoc/>
        public ICollection<ForumPost> GetPostsWithAuthors(int courseId)
        {
            return dbContext.Courses.Include(c => c.ForumPosts).ThenInclude(p => p.Author).SingleOrDefault(course => course.Id == courseId).ForumPosts;
        }

        /// <inheritdoc/>
        public ICollection<CourseMember> GetMembers(int courseId)
        {
            var courseWithMembers = dbContext.Courses.Include(course => course.Members).ThenInclude(member => member.User).Single(course => course.Id == courseId);
            return courseWithMembers.Members;
        }
    }
}