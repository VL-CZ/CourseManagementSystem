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
                .SingleOrDefault(ct => ct.Id.ToString() == testId);
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
        public void Update(string testId, int updatedWeight, string updatedTopic, DateTime updatedDeadline, ICollection<TestQuestion> updatedQuestions)
        {
            var test = GetWithQuestions(testId);

            if (IsPublished(test))
            {
                throw new ApplicationException("Cannot update already published test");
            }

            test.Weight = updatedWeight;
            test.Topic = updatedTopic;
            test.Deadline = updatedDeadline;

            RemoveAllQuestions(test);
            test.Questions = updatedQuestions;
        }

        /// <inheritdoc/>
        public TestQuestion GetQuestionByNumber(CourseTest test, int questionNumber)
        {
            return test.Questions
                .SingleOrDefault(question => question.Number == questionNumber);
        }

        /// <inheritdoc/>
        public bool IsAlreadySubmittedBy(string testId, string courseMemberId)
        {
            var courseMemberWithSubmittedTests = dbContext.CourseMembers
                .Include(cm => cm.TestSubmissions)
                .ThenInclude(ts => ts.Test)
                .SingleOrDefault(cm => cm.Id.ToString() == courseMemberId);

            return courseMemberWithSubmittedTests.TestSubmissions
                .Any(ts => ts.Test.Id.ToString() == testId);
        }

        /// <inheritdoc/>
        public IEnumerable<CourseTest> FilterActiveTests(IEnumerable<CourseTest> tests)
        {
            return tests
                .Where(test => IsPublished(test))
                .Where(test => test.Deadline > DateTime.UtcNow);
        }

        /// <inheritdoc/>
        public IEnumerable<CourseTest> FilterNonPublishedTests(IEnumerable<CourseTest> tests)
        {
            return tests
                .Where(test => !IsPublished(test));
        }

        /// <inheritdoc/>
        public IEnumerable<CourseTest> FilterTestsAfterDeadline(IEnumerable<CourseTest> tests)
        {
            return tests
                .Where(test => test.Deadline < DateTime.UtcNow);
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