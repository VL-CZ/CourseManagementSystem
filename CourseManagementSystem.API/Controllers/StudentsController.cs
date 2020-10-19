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
    public class StudentsController : ControllerBase
    {
        private readonly CMSDbContext dbContext;
        private readonly IPersonService personService;
        private readonly UserManager<Person> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StudentsController(CMSDbContext dbContext, IPersonService personService, UserManager<Person> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.personService = personService;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// get all students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PersonVM> GetAll()
        {
            return dbContext.Users.Select(p => new PersonVM() { Id = p.Id, Name = p.UserName, Email = p.Email });
        }

        /// <summary>
        /// get person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public StudentVM Get(string id)
        {
            Person p = personService.GetPersonByID(id);
            return new StudentVM
            {
                Email = p.Email,
                Id = p.Id,
                Name = p.UserName,
                Grades = p.Grades.Select(g => new GradeDetailsVM() { Id = g.ID, Comment = g.Comment, Topic = g.Topic, Value = g.Value })
            };
        }

        /// <summary>
        /// assign grade to the person with selected id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="g"></param>
        /// <returns>assigned grade</returns>
        [HttpPost("{id}/assignGrade")]
        public GradeDetailsVM AssignGrade(string id, [FromBody] AddGradeVM g)
        {
            Person p = personService.GetPersonByID(id);
            Grade grade = new Grade { Value = g.Value, Comment = g.Comment, Topic = g.Topic };
            p.AssignGrade(grade);
            dbContext.SaveChanges();

            return new GradeDetailsVM() { Id = grade.ID, Comment = grade.Comment, Value = grade.Value, Topic = grade.Topic };
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