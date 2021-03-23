using CourseManagementSystem.Data.Models;

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
    }
}