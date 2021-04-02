using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ICourseService
    {
        /// <summary>
        /// get course by its id
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        Course GetById(int courseId);
    }
}