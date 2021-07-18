using CourseManagementSystem.API.Auth;
using CourseManagementSystem.API.Auth.Attributes;
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
        [AuthorizeCourseAdminOf(EntityType.Course, "courseId")]
        public void Upload(IFormFile file, string courseId)
        {
            fileService.SaveTo(courseId, file);

            fileService.CommitChanges();
        }

        /// <summary>
        /// download the file by its Id
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet("{fileId}")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.CourseFile, "fileId")]
        public IActionResult Download(string fileId)
        {
            CourseFile file = fileService.GetFileById(fileId);
            return File(file.Data, file.ContentType, file.Name);
        }

        /// <summary>
        /// delete the selected file
        /// </summary>
        /// <param name="fileId"></param>
        [HttpDelete("delete/{fileId}")]
        [AuthorizeCourseAdminOf(EntityType.CourseFile, "fileId")]
        public void Delete(string fileId)
        {
            fileService.DeleteFileById(fileId);

            fileService.CommitChanges();
        }
    }
}