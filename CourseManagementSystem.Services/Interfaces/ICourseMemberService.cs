using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.API.Services
{
    public interface ICourseMemberService
    {
        /// <summary>
        /// get CourseMember by ID
        /// </summary>
        /// <param name="id">id of the course member</param>
        /// <returns>course member with the given id</returns>
        CourseMember GetMemberByID(int id);

        /// <summary>
        /// get CourseMember by PersonId and CourseId
        /// </summary>
        /// <param name="userId">Id of the person (user)</param>
        /// <param name="courseId">Id of the course</param>
        /// <returns><see cref="CourseMember"/> instance that belongs to the given user and course, or null</returns>
        CourseMember GetMemberByUserAndCourse(string userId, int courseId);

        /// <summary>
        /// remove person with selected ID
        /// </summary>
        /// <param name="id"></param>
        void RemoveMemberById(int id);
    }
}