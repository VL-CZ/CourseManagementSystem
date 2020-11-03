using CourseManagementSystem.API.Services;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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