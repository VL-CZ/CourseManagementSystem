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

            // remove all questions
            foreach (var question in testToRemove.Questions)
            {
                dbContext.TestQuestions.Remove(question);
            }

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
        public void Update(CourseTest test, int updatedWeight, string updatedTopic, DateTime updatedDeadline, ICollection<TestQuestion> updatedQuestions)
        {
            test.Weight = updatedWeight;
            test.Topic = updatedTopic;
            test.Deadline = updatedDeadline;
            test.Questions.Clear();
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
    }
}