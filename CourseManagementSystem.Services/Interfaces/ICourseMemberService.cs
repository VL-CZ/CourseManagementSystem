using CourseManagementSystem.Data.Models;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ICourseMemberService : ICourseReferenceService, IDbService
    {
        /// <summary>
        /// get CourseMember by its ID including <see cref="CourseMember.User"/>
        /// </summary>
        /// <param name="id">identifier of the <see cref="CourseMember"/> instance</param>
        /// <returns>course member with the given id</returns>
        CourseMember GetMemberWithUser(string id);

        /// <summary>
        /// get CourseMember by PersonId and CourseId
        /// </summary>
        /// <param name="userId">Id of the person (user)</param>
        /// <param name="courseId">Id of the course</param>
        /// <returns><see cref="CourseMember"/> instance that belongs to the given user and course, or null</returns>
        CourseMember GetMemberByUserAndCourse(string userId, string courseId);

        /// <summary>
        /// check if the course member belongs to the given person
        /// </summary>
        /// <param name="courseMemberId">identifier of the course member</param>
        /// <param name="personId">identifier of the person</param>
        /// <returns></returns>
        bool BelongsTo(string courseMemberId, string personId);

        /// <summary>
        /// archive CourseMember with selected ID
        /// </summary>
        /// <param name="id">identifier of the <see cref="CourseMember"/> instance to archive</param>
        void ArchiveMemberById(string id);

        /// <summary>
        /// get all grades of the given CourseMember
        /// </summary>
        /// <param name="courseMemberId">identifier of the <see cref="CourseMember"/></param>
        /// <returns></returns>
        IEnumerable<Grade> GetGradesOf(string courseMemberId);
    }
}