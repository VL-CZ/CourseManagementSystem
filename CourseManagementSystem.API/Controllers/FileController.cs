using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private CMSDbContext dbContext;

        public FileController(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("")]
        public IEnumerable<CourseFileVM> GetAll()
        {
            return dbContext.Files.Select(f => new CourseFileVM { Id = f.ID, Name = f.Name });
        }

        // POST api/<FileController>/upload
        [HttpPost("upload")]
        public CourseFileVM Upload(IFormFile file)
        {
            var courseFile = new CourseFile() { Name = file.FileName, ContentType = file.ContentType };
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                courseFile.Data = target.ToArray();
            }

            dbContext.Files.Add(courseFile);
            dbContext.SaveChanges();
            return new CourseFileVM() { Id = courseFile.ID, Name = courseFile.Name };
        }


        [HttpGet("{id}")]
        public FileContentResult Download(int id)
        {
            var file = dbContext.Files.Find(id);
            return File(file.Data, file.ContentType);
        }

        [HttpDelete("delete/{id}")]
        public void Delete(int id)
        {
            var file = dbContext.Files.Find(id);
            dbContext.Files.Remove(file);
            dbContext.SaveChanges();
        }

    }
}
