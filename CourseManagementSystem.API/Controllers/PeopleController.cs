using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /// enroll to course with selected Id
        /// </summary>
        /// <param name="courseId"></param>
        [HttpPost("enroll/{courseId}")]
        public void EnrollTo(string courseId)
        {
            var course = courseService.GetById(courseId);
            var currentUser = peopleService.GetById(GetCurrentUserId());

            peopleService.EnrollTo(currentUser, course);
        }

        /// <summary>
        /// get all courses, whose member the current user is
        /// </summary>
        /// <returns></returns>
        [HttpGet("memberCourses")]
        public IEnumerable<CourseInfoVM> GetMemberCourses()
        {
            var memberCourses = peopleService.GetMemberCourses(GetCurrentUserId());
            return memberCourses.Select(course => new CourseInfoVM(course.Id, course.Name));
        }

        /// <summary>
        /// get all courses, whose admin the current user is
        /// </summary>
        /// <returns></returns>
        [HttpGet("managedCourses")]
        public IEnumerable<CourseInfoVM> GetManagedCourses()
        {
            var managedCourses = peopleService.GetManagedCourses(GetCurrentUserId());
            return managedCourses.Select(c => new CourseInfoVM(c.Id, c.Name));
        }

        /// <summary>
        /// get course member id of current user in the selected course
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns></returns>
        [HttpGet("getCourseMember/{courseId}")]
        public int GetMemberByCourseId(string courseId)
        {
            var person = peopleService.GetById(GetCurrentUserId());
            var course = courseService.GetById(courseId);

            return peopleService.GetCourseMembership(person, course).Id;
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