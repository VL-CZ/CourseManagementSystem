﻿using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Http;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface IFileService : ICourseReferenceService
    {
        /// <summary>
        /// save the file into the course
        /// </summary>
        /// <param name="file"></param>
        /// <returns>saved file</returns>
        CourseFile SaveTo(string courseId, IFormFile file);

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