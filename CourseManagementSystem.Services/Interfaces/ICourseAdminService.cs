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
    }
}