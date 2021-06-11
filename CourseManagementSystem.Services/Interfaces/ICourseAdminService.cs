using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ICourseAdminService : ICourseReferenceService, IDbService
    {
        /// <summary>
        /// remove the given <see cref="CourseAdmin"/> by its ID
        /// </summary>
        /// <param name="courseAdminId">identifier of the <see cref="CourseAdmin"/></param>
        void Remove(string courseAdminId);

        /// <summary>
        /// get CourseAdmin by its ID including <see cref="CourseMember.User"/>
        /// </summary>
        /// <param name="id">identifier of the <see cref="CourseAdmin"/> instance</param>
        /// <returns>course admin with the given id</returns>
        CourseAdmin GetMemberWithUser(string id);
    }
}