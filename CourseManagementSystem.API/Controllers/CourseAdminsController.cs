using CourseManagementSystem.API.Auth;
using CourseManagementSystem.API.Auth.Attributes;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseAdminsController : ControllerBase
    {
        private readonly ICourseAdminService courseAdminService;

        public CourseAdminsController(ICourseAdminService courseAdminService)
        {
            this.courseAdminService = courseAdminService;
        }

        /// <summary>
        /// delete course admin by its id
        /// </summary>
        /// <param name="adminId">identifier of <see cref="Data.Models.CourseAdmin"/> to remove</param>
        [HttpDelete("{adminId}")]
        [AuthorizeCourseAdminOf(EntityType.CourseAdmin, "adminId")]
        public void Delete(string adminId)
        {
            courseAdminService.RemoveById(adminId);
            courseAdminService.CommitChanges();
        }
    }
}