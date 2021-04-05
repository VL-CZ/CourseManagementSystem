using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly IPeopleService peopleService;

        public CoursesController(ICourseService courseService, IPeopleService peopleService)
        {
            this.courseService = courseService;
            this.peopleService = peopleService;
        }

        /// <summary>
        /// create new course
        /// </summary>
        [HttpPost("create")]
        public void Create([FromBody] AddCourseVM courseVM)
        {
            Person admin = peopleService.GetById(courseVM.AdminId);
            Course createdCourse = new Course(courseVM.Name, admin);
            courseService.AddCourse(createdCourse);
        }

        /// <summary>
        /// delete course by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            courseService.DeleteById(id);
        }

        /// <summary>
        /// get all course members
        /// </summary>
        /// <param name="id">Id of the course</param>
        [HttpGet("{id}/members")]
        public IEnumerable<CourseMemberVM> GetAllMembers(int id)
        {
            var people = courseService.GetMembers(id);
            return people.Select(cm => new CourseMemberVM(cm.Id.ToString(), cm.User.UserName, cm.User.Email));
        }

        /// <summary>
        /// get all shared files in the course with given id
        /// </summary>
        /// <param name="id">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/files")]
        public IEnumerable<CourseFileVM> GetAllFiles(int id)
        {
            return courseService.GetFiles(id).Select(file => new CourseFileVM(file.ID, file.Name));
        }

        /// <summary>
        /// get all tests in the course with given id
        /// </summary>
        /// <param name="id">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/tests")]
        public IEnumerable<CourseTestVM> GetAllTests(int id)
        {
            var courseTests = courseService.GetTests(id);
            return courseTests.Select(test => new CourseTestVM(test.Id, test.Topic, test.Weight, test.Questions.ToViewModels(), test.Status, test.Deadline));
        }

        /// <summary>
        /// get all posts in the course with given id
        /// </summary>
        /// <param name="id">identifier of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/posts")]
        public IEnumerable<ForumPostVM> GetAllPosts(int id)
        {
            var posts = courseService.GetPostsWithAuthors(id);
            return posts.Select(post => new ForumPostVM(post.Id, post.Author.Email, post.Text));
        }
    }
}