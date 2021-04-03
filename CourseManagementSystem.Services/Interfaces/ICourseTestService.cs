using CourseManagementSystem.Data.Models;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ICourseTestService
    {
        /// <summary>
        /// add test to given course
        /// </summary>
        /// <returns></returns>
        void AddToCourse(CourseTest test, int courseId);

        /// <summary>
        /// get test with given Id
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        CourseTest GetById(int testId);

        /// <summary>
        /// delete test by its Id
        /// </summary>
        void Delete(int testId);

        /// <summary>
        /// update properties of the <paramref name="test"/>
        /// </summary>
        /// <param name="test">test to update</param>
        /// <param name="updatedWeight">updated value of weight</param>
        /// <param name="updatedTopic">updated value of topic</param>
        /// <param name="updatedQuestions">updated questions</param>
        void Update(CourseTest test, int updatedWeight, string updatedTopic, ICollection<TestQuestion> updatedQuestions);
    }
}