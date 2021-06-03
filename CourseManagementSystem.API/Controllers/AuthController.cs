using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPeopleService peopleService;
        private readonly ICourseMemberService courseMemberService;
        private readonly ICourseTestService courseTestService;

        public AuthController(IHttpContextAccessor httpContextAccessor, 
            IPeopleService peopleService, ICourseMemberService courseMemberService, ICourseTestService courseTestService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.peopleService = peopleService;
            this.courseMemberService = courseMemberService;
            this.courseTestService = courseTestService;
        }

        /// <summary>
        /// get current user id
        /// </summary>
        /// <returns></returns>
        [HttpGet("getId")]
        public WrapperVM<string> GetId()
        {
            string userId = GetCurrentUserId();
            return new WrapperVM<string>(userId);
        }

        /// <summary>
        /// determine if current user is admin of the selected course
        /// </summary>
        /// <returns></returns>
        [HttpGet("isCourseAdmin/{courseId}")]
        public WrapperVM<bool> IsCourseAdmin(string courseId)
        {
            bool isCourseAdmin = peopleService.IsAdminOfCourse(GetCurrentUserId(), courseId);
            return new WrapperVM<bool>(isCourseAdmin);
        }

        /// <summary>
        /// determine if current user is admin of the course that contains selected <see cref="Data.Models.CourseMember"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet("isCourseMemberAdmin/{courseMemberId}")]
        public WrapperVM<bool> IsCourseMemberAdmin(string courseMemberId)
        {
            string courseId = courseMemberService.GetCourseIdOf(courseMemberId);
            bool isCourseAdmin = peopleService.IsAdminOfCourse(GetCurrentUserId(), courseId);
            return new WrapperVM<bool>(isCourseAdmin);
        }

        /// <summary>
        /// determine if current user is admin of the course that contains selected <see cref="Data.Models.CourseTest"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet("isCourseTestAdmin/{testId}")]
        public WrapperVM<bool> IsCourseTestAdmin(string testId)
        {
            string courseId = courseTestService.GetCourseIdOf(testId);
            bool isCourseAdmin = peopleService.IsAdminOfCourse(GetCurrentUserId(), courseId);
            return new WrapperVM<bool>(isCourseAdmin);
        }

        /// <summary>
        /// get ID of the current user
        /// </summary>
        /// <returns></returns>
        private string GetCurrentUserId()
        {
            return httpContextAccessor.HttpContext.GetCurrentUserId();
        }
    }
}