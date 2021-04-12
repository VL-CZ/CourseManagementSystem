using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class CourseTestService : DbService, ICourseTestService
    {
        public CourseTestService(CMSDbContext dbContext) : base(dbContext)
        {
        }

        /// <inheritdoc/>
        public void AddToCourse(CourseTest test, string courseId)
        {
            var course = dbContext.Courses.FindById(courseId);
            course.Tests.Add(test);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void Delete(string testId)
        {
            var testToRemove = GetById(testId);
            dbContext.CourseTests.Remove(testToRemove);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public string GetCourseId(string testId)
        {
            return GetById(testId).Course.Id.ToString();
        }

        /// <inheritdoc/>
        public CourseTest GetById(string testId)
        {
            return dbContext.CourseTests.Include(ct => ct.Course).Include(ct => ct.Questions).SingleOrDefault(ct => ct.Id.ToString() == testId);
        }

        /// <inheritdoc/>
        public void Publish(CourseTest test)
        {
            test.Status = TestStatus.Published;
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void Update(CourseTest test, int updatedWeight, string updatedTopic, DateTime updatedDeadline, ICollection<TestQuestion> updatedQuestions)
        {
            test.Weight = updatedWeight;
            test.Topic = updatedTopic;
            test.Deadline = updatedDeadline;
            test.Questions.Clear();
            test.Questions = updatedQuestions;

            dbContext.SaveChanges();
        }
    }
}