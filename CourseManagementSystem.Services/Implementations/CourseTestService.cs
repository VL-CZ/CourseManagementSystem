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
        }

        /// <inheritdoc/>
        public void Delete(string testId)
        {
            var testToRemove = GetWithQuestions(testId);

            if (IsPublished(testToRemove))
            {
                throw new ApplicationException("Cannot remove already published test");
            }

            RemoveAllQuestions(testToRemove);

            // remove the test
            dbContext.CourseTests.Remove(testToRemove);
        }

        /// <inheritdoc/>
        public CourseTest GetWithQuestions(string testId)
        {
            return dbContext.CourseTests
                .Include(test => test.Questions)
                .Single(ct => ct.Id.ToString() == testId);
        }

        ///<inheritdoc/>
        public string GetCourseIdOf(string objectId)
        {
            return dbContext.CourseTests.GetCourseIdOf(objectId);
        }

        /// <inheritdoc/>
        public void Publish(CourseTest test)
        {
            test.Status = TestStatus.Published;
        }

        /// <inheritdoc/>
        public void Update(string testId, int updatedWeight, string updatedTopic, DateTime updatedDeadline, ICollection<TestQuestion> updatedQuestions, bool updatedIsGraded)
        {
            var test = GetWithQuestions(testId);

            if (IsPublished(test))
            {
                throw new ApplicationException("Cannot update already published test");
            }

            test.Weight = updatedWeight;
            test.Topic = updatedTopic;
            test.Deadline = updatedDeadline;
            test.IsGraded = updatedIsGraded;

            RemoveAllQuestions(test);
            test.Questions = updatedQuestions;
        }

        /// <summary>
        /// check if the test is published
        /// </summary>
        /// <param name="test">test to check</param>
        /// <returns></returns>
        private bool IsPublished(CourseTest test)
        {
            return test.Status == TestStatus.Published;
        }

        /// <summary>
        /// remove all questions of the test
        /// </summary>
        /// <param name="test"></param>
        private void RemoveAllQuestions(CourseTest test)
        {
            foreach (var question in test.Questions)
            {
                dbContext.TestQuestions.Remove(question);
            }
        }
    }
}