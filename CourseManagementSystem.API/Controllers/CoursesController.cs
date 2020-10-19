using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private static int nextId = 1;
        private static List<Course> courses = new List<Course>();

        /// <summary>
        /// get all courses
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IEnumerable<CourseInfoVM> GetAll()
        {
            return courses.Select(c => new CourseInfoVM(c.Id, c.Name));
        }

        /// <summary>
        /// create new course
        /// </summary>
        /// <returns></returns>
        [HttpPost("create")]
        public CourseInfoVM Create([FromBody] AddCourseVM courseVM)
        {
            courses.Add(new Course(courseVM.Name, null) { Id = nextId });
            var c = Get(nextId);

            nextId++;
            return new CourseInfoVM(c.Id, c.Name);
        }

        /// <summary>
        /// get course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Course Get(int id)
        {
            return courses.Single(x => x.Id == id);
        }

        /// <summary>
        /// delete course by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Course c = courses.Single(x => x.Id == id);
            courses.Remove(c);
        }
    }
}