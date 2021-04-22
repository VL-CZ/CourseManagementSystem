using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Http;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface IFileService : ICourseReferenceService, IDbService
    {
        /// <summary>
        /// save the file into the course
        /// </summary>
        /// <param name="courseId">identifier of the course where to save the file</param>
        /// <param name="file">file to save</param>
        void SaveTo(string courseId, IFormFile file);

        /// <summary>
        /// delete file with selected Id
        /// </summary>
        /// <param name="id"></param>
        void DeleteFileById(string id);

        /// <summary>
        /// get file by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CourseFile GetFileById(string id);
    }
}