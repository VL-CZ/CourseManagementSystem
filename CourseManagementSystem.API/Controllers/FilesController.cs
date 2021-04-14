using CourseManagementSystem.API.Auth;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IFileService fileService;

        public FilesController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        /// <summary>
        /// upload the file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload/{courseId}")]
        [AuthorizeCourseAdminOf(EntityType.CourseTest, "courseId")]
        public void Upload(IFormFile file, string courseId)
        {
            fileService.SaveTo(courseId, file);
        }

        /// <summary>
        /// download the file by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public FileContentResult Download(string id)
        {
            CourseFile file = fileService.GetFileById(id);
            return File(file.Data, file.ContentType, file.Name);
        }

        /// <summary>
        /// delete the selected file
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("delete/{id}")]
        [AuthorizeCourseAdminOf(EntityType.CourseFile, "id")]
        public void Delete(string id)
        {
            fileService.DeleteFileById(id);
        }
    }
}