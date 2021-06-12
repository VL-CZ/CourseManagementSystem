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
    }
}