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

        /// <inheritdoc/>
        public CourseFile SaveTo(string courseId, IFormFile file)
        {
            var courseFile = new CourseFile() { Name = file.FileName, ContentType = file.ContentType };
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                courseFile.Data = target.ToArray();
            }

            Course c = dbContext.Courses.Include(c => c.Files).Single(c => c.Id.ToString() == courseId);

            c.Files.Add(courseFile);
            dbContext.SaveChanges();

            return courseFile;
        }
    }
}