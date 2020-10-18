using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
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
        private readonly CMSDbContext dbContext;
        private readonly IFileService fileService;

        public FileController(CMSDbContext dbContext, IFileService fileService)
        {
            this.dbContext = dbContext;
            this.fileService = fileService;
        }

        /// <summary>
        /// get all file information
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IEnumerable<CourseFileVM> GetAll()
        {
            return dbContext.Files.Select(f => new CourseFileVM { Id = f.ID, Name = f.Name });
        }


        /// <summary>
        /// upload the file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public CourseFileVM Upload(IFormFile file)
        {
            CourseFile courseFile = fileService.Save(file);
            return new CourseFileVM() { Id = courseFile.ID, Name = courseFile.Name };
        }


        /// <summary>
        /// download the file by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public FileContentResult Download(int id)
        {
            CourseFile file = fileService.GetFileById(id);
            return File(file.Data, file.ContentType, file.Name);
        }

        /// <summary>
        /// delete the selected file
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("delete/{id}")]
        public void Delete(int id)
        {
            fileService.DeleteFileById(id);
        }

    }
}
