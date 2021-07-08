using CourseManagementSystem.API.Auth;
using CourseManagementSystem.API.Auth.Attributes;
using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseAdminsController : ControllerBase
    {
        private readonly ICourseAdminService courseAdminService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CourseAdminsController(ICourseAdminService courseAdminService, IHttpContextAccessor httpContextAccessor)
        {
            this.courseAdminService = courseAdminService;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// delete course admin by its id
        /// </summary>
        /// <param name="adminId">identifier of <see cref="Data.Models.CourseAdmin"/> to remove</param>
        [HttpDelete("{adminId}")]
        [AuthorizeCourseAdminOf(EntityType.CourseAdmin, "adminId")]
        public void Delete(string adminId)
        {
            var adminPersonId = courseAdminService.GetPersonId(adminId);
            
            // we cannot remove ourselves
            if (adminPersonId == httpContextAccessor.HttpContext.GetCurrentUserId())
            {
                throw new ApplicationException("Cannot remove own admin membership");
            }

            courseAdminService.RemoveById(adminId);
            courseAdminService.CommitChanges();
        }
    }
}