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
        void Update(string testId, int updatedWeight, string updatedTopic, DateTime updatedDeadline, ICollection<TestQuestion> updatedQuestions);

        /// <summary>
        /// publish the test (set <see cref="CourseTest.Status"/> to <see cref="TestStatus.Published"/>)
        /// </summary>
        /// <param name="test">test to publish</param>
        void Publish(CourseTest test);

        /// <summary>
        /// get question in the test by its number
        /// </summary>
        /// <param name="test">given test</param>
        /// <param name="questionNumber">number of the question</param>
        /// <returns></returns>
        TestQuestion GetQuestionByNumber(CourseTest test, int questionNumber);

        /// <summary>
        /// check if the course member has already submitted the test
        /// </summary>
        /// <param name="testId">identifier of the test</param>
        /// <param name="courseMemberId">identifier of the courseMember</param>
        /// <returns></returns>
        bool IsAlreadySubmittedBy(string testId, string courseMemberId);

        /// <summary>
        /// filter active tests from <paramref name="tests"/>
        /// <br/>
        /// a test is active if it's published and before deadline
        /// </summary>
        /// <param name="tests">tests to filter</param>
        /// <returns></returns>
        IEnumerable<CourseTest> FilterActiveTests(IEnumerable<CourseTest> tests);

        /// <summary>
        /// filter tests that haven't been published yet from <paramref name="tests"/>
        /// </summary>
        /// <param name="tests">tests to filter</param>
        /// <returns></returns>
        IEnumerable<CourseTest> FilterNonPublishedTests(IEnumerable<CourseTest> tests);

        /// <summary>
        /// filter tests that are after deadline from <paramref name="tests"/>
        /// </summary>
        /// <param name="tests">tests to filter</param>
        /// <returns></returns>
        IEnumerable<CourseTest> FilterTestsAfterDeadline(IEnumerable<CourseTest> tests);
    }
}