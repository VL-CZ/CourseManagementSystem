using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CourseManagementSystem.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly CMSDbContext dbContext;

        public FileService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void DeleteFileById(int id)
        {
            var file = dbContext.Files.Find(id);
            dbContext.Files.Remove(file);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public CourseFile GetFileById(int id)
        {
            return dbContext.Files.Find(id);
        }

        /// <inheritdoc/>
        public CourseFile SaveTo(int courseId, IFormFile file)
        {
            var courseFile = new CourseFile() { Name = file.FileName, ContentType = file.ContentType };
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                courseFile.Data = target.ToArray();
            }

            Course c = dbContext.Courses.Include(c => c.Files).Single(c => c.Id == courseId);
                
            c.Files.Add(courseFile);
            dbContext.SaveChanges();

            return courseFile;
        }
    }
}
