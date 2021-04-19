using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class FileService : DbService, IFileService
    {
        public FileService(CMSDbContext dbContext):base(dbContext)
        {
        }

        /// <inheritdoc/>
        public void DeleteFileById(string id)
        {
            var file = GetFileById(id);
            dbContext.Files.Remove(file);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public CourseFile GetFileById(string id)
        {
            return dbContext.Files.FindById(id);
        }

        ///<inheritdoc/>
        public string GetCourseIdOf(string objectId)
        {
            return dbContext.Files.GetCourseIdOf(objectId);
        }

        /// <inheritdoc/>
        public CourseFile SaveTo(string courseId, IFormFile file)
        {
            byte[] fileData;
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                fileData = target.ToArray();
            }

            var courseFile = new CourseFile(fileData, file.Name, file.ContentType);

            Course c = dbContext.Courses.Include(c => c.Files).Single(c => c.Id.ToString() == courseId);

            c.Files.Add(courseFile);
            dbContext.SaveChanges();

            return courseFile;
        }
    }
}