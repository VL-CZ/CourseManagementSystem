using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// save the file
        /// </summary>
        /// <param name="file"></param>
        /// <returns>saved file</returns>
        CourseFile Save(IFormFile file);

        /// <summary>
        /// delete file with selected Id
        /// </summary>
        /// <param name="id"></param>
        void DeleteFileById(int id);

        /// <summary>
        /// get file by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CourseFile GetFileById(int id);
    }
}
