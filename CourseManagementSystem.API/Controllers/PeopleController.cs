using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PeopleController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPeopleService peopleService;
        private readonly ICourseService courseService;

        public PeopleController(IHttpContextAccessor httpContextAccessor, IPeopleService peopleService, ICourseService courseService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.peopleService = peopleService;
            this.courseService = courseService;
        }

        /// <summary>
        /// get all courses, whose member the current user is
        /// </summary>
        /// <returns></returns>
        [HttpGet("memberCourses")]
        public IEnumerable<CourseInfoVM> GetMemberCourses()
        {
            var memberCourses = peopleService.GetActiveMemberCourses(GetCurrentUserId());
            return memberCourses.Select(course => new CourseInfoVM(course.Id.ToString(), course.Name));
        }

        /// <summary>
        /// get all courses, whose admin the current user is
        /// </summary>
        /// <returns></returns>
        [HttpGet("managedCourses")]
        public IEnumerable<CourseInfoVM> GetManagedCourses()
        {
            var managedCourses = peopleService.GetActiveManagedCourses(GetCurrentUserId());
            return managedCourses.Select(course => new CourseInfoVM(course.Id.ToString(), course.Name));
        }

        /// <summary>
        /// get course member id of current user in the selected course
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns></returns>
        [HttpGet("getCourseMember/{courseId}")]
        public WrapperVM<string> GetMemberByCourseId(string courseId)
        {
            var person = peopleService.GetById(GetCurrentUserId());
            var course = courseService.GetById(courseId);

            var courseMember = peopleService.GetCourseMembership(person, course);
            return new WrapperVM<string>(courseMember.Id.ToString());
        }

        /// <summary>
        /// get Id of the currently logged-in user
        /// </summary>
        /// <returns></returns>
        private string GetCurrentUserId()
        {
            return httpContextAccessor.HttpContext.GetCurrentUserId();
        }
    }
}