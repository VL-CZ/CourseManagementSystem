using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ICourseTestService : ICourseReferenceService, IDbService
    {
        /// <summary>
        /// add test to given course
        /// </summary>
        /// <returns></returns>
        void AddToCourse(CourseTest test, string courseId);

        /// <summary>
        /// get test with the given Id including <see cref="CourseTest.Questions"/>
        /// </summary>
        /// <param name="testId">identifier of the <see cref="CourseTest"/></param>
        /// <returns></returns>
        CourseTest GetWithQuestions(string testId);

        /// <summary>
        /// delete test by its Id
        /// </summary>
        void Delete(string testId);

        /// <summary>
        /// update properties of the <paramref name="test"/>
        /// </summary>
        /// <param name="testId">identifier of the test to update</param>
        /// <param name="updatedWeight">updated value of weight</param>
        /// <param name="updatedTopic">updated value of topic</param>
        /// <param name="updatedDeadline">updated value of deadline</param>
        /// <param name="updatedQuestions">updated questions</param>
        /// <param name="updatedIsGraded">updated value of IsGraded</param>
        void Update(string testId, int updatedWeight, string updatedTopic, DateTime updatedDeadline, ICollection<TestQuestion> updatedQuestions, bool updatedIsGraded);

        /// <summary>
        /// publish the test (set <see cref="CourseTest.Status"/> to <see cref="TestStatus.Published"/>)
        /// </summary>
        /// <param name="test">test to publish</param>
        void Publish(CourseTest test);
    }
}