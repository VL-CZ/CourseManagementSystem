using CourseManagementSystem.API.Auth;
using CourseManagementSystem.API.Auth.Attributes;
using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly IPeopleService peopleService;
        private readonly ICourseTestService courseTestService;

        public CoursesController(ICourseService courseService, IPeopleService peopleService, ICourseTestService courseTestService)
        {
            this.courseService = courseService;
            this.peopleService = peopleService;
            this.courseTestService = courseTestService;
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

            courseService.CommitChanges();
        }

        /// <summary>
        /// delete course by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [AuthorizeCourseAdminOf(EntityType.Course, "id")]
        public void Delete(string id)
        {
            courseService.ArchiveById(id);
            courseService.CommitChanges();
        }

        /// <summary>
        /// get all course members
        /// </summary>
        /// <param name="id">Id of the course</param>
        [HttpGet("{id}/members")]
        [AuthorizeCourseAdminOf(EntityType.Course, "id")]
        public IEnumerable<CourseMemberVM> GetAllMembers(string id)
        {
            var people = courseService.GetMembersWithUsers(id);
            return people.Select(cm => new CourseMemberVM(cm.Id.ToString(), cm.User.UserName, cm.User.Email));
        }

        /// <summary>
        /// get all shared files in the course with given id
        /// </summary>
        /// <param name="id">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/files")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.Course, "id")]
        public IEnumerable<CourseFileVM> GetAllFiles(string id)
        {
            return courseService.GetFiles(id).Select(file => new CourseFileVM(file.Id.ToString(), file.Name));
        }

        /// <summary>
        /// get all active tests in the course with given id
        /// </summary>
        /// <param name="id">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/tests")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.Course, "id")]
        public IEnumerable<CourseTestDetailsVM> GetActiveTests(string id)
        {
            var courseTests = courseService.GetTests(id);
            var activeTests = courseTestService.FilterActiveTests(courseTests);
            return activeTests.Select(test => new CourseTestDetailsVM(test.Id.ToString(), test.Topic, test.Weight, test.Questions.ToViewModels(), test.Status, test.Deadline));
        }

        /// <summary>
        /// get all posts in the course with given id
        /// </summary>
        /// <param name="id">identifier of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/posts")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.Course, "id")]
        public IEnumerable<ForumPostVM> GetAllPosts(string id)
        {
            var posts = courseService.GetPostsWithAuthors(id);
            return posts.Select(post => new ForumPostVM(post.Id.ToString(), post.Author.Email, post.Text));
        }
    }
}