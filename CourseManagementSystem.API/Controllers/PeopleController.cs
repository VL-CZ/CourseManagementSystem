using CourseManagementSystem.API.Services;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly CMSDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PeopleController(CMSDbContext dbContext, ICourseMemberService personService, UserManager<Person> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// enroll to course with selected Id
        /// </summary>
        /// <param name="courseId"></param>
        [HttpPost("enroll/{courseId}")]
        public void EnrollTo(int courseId)
        {
            var course = dbContext.Courses.Find(courseId);
            var user = dbContext.Users.Find(GetCurrentUserId());

            var cm = new CourseMember() { Course = course, User = user };
            dbContext.CourseMembers.Add(cm);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// get all courses, whose member the current user is
        /// </summary>
        /// <returns></returns>
        [HttpGet("memberCourses")]
        public IEnumerable<CourseInfoVM> GetMemberCourses()
        {
            var courseMembershipIds = dbContext.Users.Include(u => u.CourseMemberships).Single(u => u.Id == GetCurrentUserId()).CourseMemberships.Select(cm => cm.Id);
            var courseVMs = dbContext.CourseMembers.Include(cm => cm.Course)
                .Where(cm => courseMembershipIds.Contains(cm.Id))
                .Select(cm => new CourseInfoVM(cm.Course.Id, cm.Course.Name));

            return courseVMs;
        }

        /// <summary>
        /// get all courses, whose admin the current user is
        /// </summary>
        /// <returns></returns>
        [HttpGet("managedCourses")]
        public IEnumerable<CourseInfoVM> GetManagedCourses()
        {
            var managedCourses = dbContext.Courses.Include(c => c.Admin).Where(c => c.Admin.Id == GetCurrentUserId());
            return managedCourses.Select(c => new CourseInfoVM(c.Id, c.Name));
        }

        /// <summary>
        /// get course member object of current user in the selected course
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns></returns>
        [HttpGet("getCourseMember/{courseId}")]
        public int GetMemberByCourseId(int courseId)
        {
            var currentUserId = GetCurrentUserId();
            return dbContext.CourseMembers.Include(cm => cm.Course).Include(cm => cm.User)
                .Where(cm => cm.User.Id == GetCurrentUserId())
                .Where(cm => cm.Course.Id == courseId)
                .Select(cm => cm.Id)
                .Single();
        }

        /// <summary>
        /// get current user id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("getId")]
        public PersonIdVM GetId()
        {
            string userId = GetCurrentUserId();
            return new PersonIdVM() { Id = userId };
        }

        /// <summary>
        /// determine if current user is admin
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("isAdmin")]
        public IsAdminVM IsAdmin()
        {
            // TO-DO: add roles
            return new IsAdminVM() { IsAdmin = GetCurrentUserId() == "b7a6f405-c226-4f5a-a0cb-2ba4c47582a3" };
        }

        private string GetCurrentUserId()
        {
            return httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}