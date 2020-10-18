using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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

        // POST api/<FileController>/upload
        [HttpPost("upload")]
        public int Upload(IFormFile file)
        {
            var courseFile = new CourseFile();
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                courseFile.Data = target.ToArray();
            }

            dbContext.Files.Add(courseFile);
            dbContext.SaveChanges();

            return courseFile.ID;
        }


    }
}
