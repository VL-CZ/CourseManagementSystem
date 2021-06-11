using CourseManagementSystem.API.Auth;
using CourseManagementSystem.API.Auth.Attributes;
using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.Tools;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
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
    public class CoursesController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICourseService courseService;
        private readonly IPeopleService peopleService;
        private CourseTestFilter courseTestFilter;

        public CoursesController(IHttpContextAccessor httpContextAccessor, ICourseService courseService, IPeopleService peopleService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.courseService = courseService;
            this.peopleService = peopleService;
            courseTestFilter = new CourseTestFilter();
        }

        /// <summary>
        /// create new course
        /// </summary>
        [HttpPost("create")]
        public void Create(AddCourseVM courseVM)
        {
            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
            Person admin = peopleService.GetById(currentUserId);
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
        /// add another admin to a course with selected Id
        /// </summary>
        /// <param name="courseId"></param>
        [HttpPost("{courseId}/addAdmin")]
        [AuthorizeCourseAdminOf(EntityType.Course, "courseId")]
        public void AddAdminTo(string courseId, WrapperVM<string> adminId)
        {
            var newAdmin = peopleService.GetById(adminId.Value);
            courseService.AddAdmin(newAdmin, courseId);
            courseService.CommitChanges();
        }

        /// <summary>
        /// enroll current user to a course with selected Id
        /// </summary>
        /// <param name="courseId"></param>
        [HttpPost("{courseId}/enroll")]
        public void EnrollTo(string courseId)
        {
            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
            var currentUser = peopleService.GetById(currentUserId);

            courseService.Enroll(currentUser, courseId);

            courseService.CommitChanges();
        }

        /// <summary>
        /// get all course members
        /// </summary>
        /// <param name="id">Id of the course</param>
        [HttpGet("{id}/members")]
        [AuthorizeCourseAdminOf(EntityType.Course, "id")]
        public IEnumerable<CourseMemberOrAdminVM> GetAllMembers(string id)
        {
            var people = courseService.GetMembersWithUsers(id);
            return people.Select(cm => new CourseMemberOrAdminVM(cm.Id.ToString(), cm.User.UserName, cm.User.Email));
        }

        /// <summary>
        /// get all course admins
        /// </summary>
        /// <param name="id">Id of the course</param>
        [HttpGet("{id}/admins")]
        [AuthorizeCourseAdminOf(EntityType.Course, "id")]
        public IEnumerable<CourseMemberOrAdminVM> GetAllAdmins(string id)
        {
            var admins = courseService.GetAdminsWithUsers(id);
            return admins.Select(admin => new CourseMemberOrAdminVM(admin.Id.ToString(), admin.User.UserName, admin.User.Email));
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
        /// get all ACTIVE tests in the course with given id
        /// </summary>
        /// <param name="id">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/activeTests")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.Course, "id")]
        public IEnumerable<CourseTestDetailsVM> GetActiveTests(string id)
        {
            return GetAndFilterTests(id, courseTestFilter.FilterActive);
        }

        /// <summary>
        /// get all tests in the given course that haven't been published yet
        /// </summary>
        /// <param name="id">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/nonPublishedTests")]
        [AuthorizeCourseAdminOf(EntityType.Course, "id")]
        public IEnumerable<CourseTestDetailsVM> GetNonPublishedTests(string id)
        {
            return GetAndFilterTests(id, courseTestFilter.FilterNonPublished);
        }

        /// <summary>
        /// get all tests in the given course that are after deadline
        /// </summary>
        /// <param name="id">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/testsAfterDeadline")]
        [AuthorizeCourseAdminOf(EntityType.Course, "id")]
        public IEnumerable<CourseTestDetailsVM> GetTestsAfterDeadline(string id)
        {
            return GetAndFilterTests(id, courseTestFilter.FilterAfterDeadline);
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

        /// <summary>
        /// get and filter the tests in the course
        /// </summary>
        /// <param name="courseId">identifier of the course that contains these tests</param>
        /// <param name="filter">function to filter the tests</param>
        /// <returns></returns>
        private IEnumerable<CourseTestDetailsVM> GetAndFilterTests(string courseId, TestFilterDelegate filter)
        {
            var courseTests = courseService.GetTests(courseId);
            var filteredTests = filter(courseTests);
            return filteredTests.Select(test =>
                new CourseTestDetailsVM(test.Id.ToString(), test.Topic, test.Weight, test.Questions.ToViewModels(), test.Status, test.Deadline, test.IsGraded));
        }

        /// <summary>
        /// delegate for filtering tests
        /// </summary>
        /// <param name="tests">tests to filter</param>
        /// <returns></returns>
        public delegate IEnumerable<CourseTest> TestFilterDelegate(IEnumerable<CourseTest> tests);
    }
}