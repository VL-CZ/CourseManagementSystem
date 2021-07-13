using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ICourseAdminService : ICourseReferenceService, IDbService
    {
        /// <summary>
        /// remove <see cref="CourseAdmin"/> entity with selected ID
        /// </summary>
        /// <param name="courseAdminId">identifier of the <see cref="CourseAdmin"/> to remove</param>
        void RemoveById(string courseAdminId);

        /// <summary>
        /// get identifier of <see cref="Person"/> that this course admin object belongs to
        /// </summary>
        /// <param name="courseAdminId">identifier of the course admin object</param>
        /// <returns></returns>
        string GetPersonId(string courseAdminId);

        /// <summary>
        /// get <see cref="CourseAdmin"/> instance that belongs to the selected course and person
        /// </summary>
        /// <param name="userId">identifier of the user</param>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        CourseAdmin GetByUserAndCourse(string userId, string courseId);
    }
}