using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ICourseMemberService
    {
        /// <summary>
        /// get CourseMember by ID
        /// </summary>
        /// <param name="id">identifier of the <see cref="CourseMember"/> instance</param>
        /// <returns>course member with the given id</returns>
        CourseMember GetMemberByID(string id);

        /// <summary>
        /// get CourseMember by PersonId and CourseId
        /// </summary>
        /// <param name="userId">Id of the person (user)</param>
        /// <param name="courseId">Id of the course</param>
        /// <returns><see cref="CourseMember"/> instance that belongs to the given user and course, or null</returns>
        CourseMember GetMemberByUserAndCourse(string userId, string courseId);

        /// <summary>
        /// remove person with selected ID
        /// </summary>
        /// <param name="id">identifier of the <see cref="CourseMember"/> instance</param>
        void RemoveMemberById(string id);

        /// <summary>
        /// assign new <see cref="Grade"/> to the given <see cref="CourseMember"/> instance
        /// </summary>
        /// <param name="courseMember">course member, which we add the <paramref name="grade"/> to</param>
        /// <param name="grade">grade to add to <paramref name="courseMember"/></param>
        void AssignGrade(CourseMember courseMember, Grade grade);
    }
}