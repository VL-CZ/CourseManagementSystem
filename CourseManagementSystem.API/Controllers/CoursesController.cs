using CourseManagementSystem.API.Services;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CMSDbContext dbContext;

        public CoursesController(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// create new course
        /// </summary>
        /// <returns></returns>
        [HttpPost("create")]
        public CourseInfoVM Create([FromBody] AddCourseVM courseVM)
        {
            Person admin = dbContext.Users.Single(x => x.Id == courseVM.AdminId);
            Course createdCourse = new Course(courseVM.Name, admin);

            dbContext.Courses.Add(createdCourse);
            dbContext.SaveChanges();

            return new CourseInfoVM(createdCourse.Id, createdCourse.Name);
        }

        /// <summary>
        /// get course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Course Get(int id)
        {
            return dbContext.Courses.Find(id);
        }

        /// <summary>
        /// delete course by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Course c = dbContext.Courses.Find(id);
            dbContext.Courses.Remove(c);
        }

        /// <summary>
        /// get all course members
        /// </summary>
        [HttpGet("{id}/members")]
        public IEnumerable<PersonVM> GetAllMembers(int id)
        {
            var course = dbContext.Courses.Include(x => x.Members).Include(x => x.Members.Select(m => m.User)).Single(x => x.Id == id);

            return course.Members.Select(cm => new PersonVM() { Id = cm.Id.ToString(), Name = cm.User.UserName, Email = cm.User.Email });
        }

        /// <summary>
        /// get all course files
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/files")]
        public IEnumerable<CourseFileVM> GetAll(int id)
        {
            return dbContext.Courses.Single(x => x.Id == id).Files.Select(f => new CourseFileVM { Id = f.ID, Name = f.Name });
        }
    }
}