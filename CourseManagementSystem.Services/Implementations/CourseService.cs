﻿using CourseManagementSystem.Data;
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
        public void DeleteById(string courseId)
        {
            Course c = GetById(courseId);
            dbContext.Courses.Remove(c);
            dbContext.SaveChanges();
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
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public ICollection<CourseFile> GetFiles(string courseId)
        {
            return dbContext.Courses.Include(c => c.Files).Single(c => c.Id.ToString() == courseId).Files;
        }

        /// <inheritdoc/>
        public ICollection<CourseTest> GetTests(string courseId)
        {
            return dbContext.Courses.Include(course => course.Tests).Single(course => course.Id.ToString() == courseId).Tests;
        }

        /// <inheritdoc/>
        public ICollection<ForumPost> GetPostsWithAuthors(string courseId)
        {
            return dbContext.Courses.Include(c => c.ForumPosts).ThenInclude(p => p.Author).SingleOrDefault(course => course.Id.ToString() == courseId).ForumPosts;
        }

        /// <inheritdoc/>
        public ICollection<CourseMember> GetMembers(string courseId)
        {
            var courseWithMembers = dbContext.Courses.Include(course => course.Members).ThenInclude(member => member.User)
                .Single(course => course.Id.ToString() == courseId);
            return courseWithMembers.Members;
        }
    }
}