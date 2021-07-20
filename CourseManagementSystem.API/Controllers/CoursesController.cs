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
using System;
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
        private readonly ICourseMemberService courseMemberService;
        private readonly ICourseAdminService courseAdminService;
        private readonly IEnrollmentRequestService enrollmentRequestService;
        private CourseTestFilter courseTestFilter;

        public CoursesController(IHttpContextAccessor httpContextAccessor, ICourseService courseService, IPeopleService peopleService,
            ICourseMemberService courseMemberService, ICourseAdminService courseAdminService, IEnrollmentRequestService enrollmentRequestService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.courseService = courseService;
            this.peopleService = peopleService;
            this.courseAdminService = courseAdminService;
            this.courseMemberService = courseMemberService;
            this.enrollmentRequestService = enrollmentRequestService;
            courseTestFilter = new CourseTestFilter();
        }

        /// <summary>
        /// get course info by its identifier
        /// </summary>
        /// <param name="courseId">id of the course to return</param>
        /// <returns></returns>
        [AuthorizeCourseAdminOrMemberOf(EntityType.Course, "courseId")]
        [HttpGet("{courseId}")]
        public CourseInfoVM GetById(string courseId)
        {
            var selectedCourse = courseService.GetById(courseId);
            return new CourseInfoVM(selectedCourse.Id.ToString(), selectedCourse.Name);
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
        /// <param name="courseId"></param>
        [HttpDelete("{courseId}")]
        [AuthorizeCourseAdminOf(EntityType.Course, "courseId")]
        public void Delete(string courseId)
        {
            courseService.ArchiveById(courseId);
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
            if (peopleService.IsAdminOfCourse(adminId.Value, courseId))
            {
                throw new ApplicationException("This user is already admin of the course");
            }

            var newAdmin = peopleService.GetById(adminId.Value);
            courseService.AddAdmin(newAdmin, courseId);
            courseService.CommitChanges();
        }

        /// <summary>
        /// request enrollment of current user to a course with selected Id
        /// </summary>
        /// <param name="courseId"></param>
        [HttpPost("{courseId}/enroll")]
        public void RequestEnrollment(string courseId)
        {
            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();

            if (peopleService.IsAdminOfCourse(currentUserId, courseId) || peopleService.IsMemberOfCourse(currentUserId, courseId))
            {
                throw new ArgumentException("Cannot enroll to the selected course. The user is already admin/member of this course.");
            }
            if (enrollmentRequestService.HasRequestedEnrollment(currentUserId, courseId))
            {
                throw new ArgumentException("The user has already requested enrollment to the given course");
            }

            var currentUser = peopleService.GetById(currentUserId);

            courseService.RequestEnrollment(currentUser, courseId);

            courseService.CommitChanges();
        }

        /// <summary>
        /// get all course members
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        [HttpGet("{courseId}/members")]
        [AuthorizeCourseAdminOf(EntityType.Course, "courseId")]
        public IEnumerable<CourseMemberOrAdminVM> GetAllMembers(string courseId)
        {
            var people = courseService.GetMembersWithUsers(courseId);
            return people.Select(cm => new CourseMemberOrAdminVM(cm.Id.ToString(), cm.User.UserName, cm.User.Email));
        }

        /// <summary>
        /// get all course admins
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        [HttpGet("{courseId}/admins")]
        [AuthorizeCourseAdminOf(EntityType.Course, "courseId")]
        public IEnumerable<CourseMemberOrAdminVM> GetAllAdmins(string courseId)
        {
            var admins = courseService.GetAdminsWithUsers(courseId);
            return admins.Select(admin => new CourseMemberOrAdminVM(admin.Id.ToString(), admin.User.UserName, admin.User.Email));
        }

        /// <summary>
        /// get all shared files in the course with given id
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{courseId}/files")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.Course, "courseId")]
        public IEnumerable<CourseFileVM> GetAllFiles(string courseId)
        {
            return courseService.GetFiles(courseId).Select(file => new CourseFileVM(file.Id.ToString(), file.Name));
        }

        /// <summary>
        /// get all ACTIVE tests in the course with given id
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{courseId}/activeTests")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.Course, "courseId")]
        public IEnumerable<CourseTestDetailsVM> GetActiveTests(string courseId)
        {
            return GetAndFilterTests(courseId, courseTestFilter.FilterActive);
        }

        /// <summary>
        /// get all tests in the given course that haven't been published yet
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{courseId}/nonPublishedTests")]
        [AuthorizeCourseAdminOf(EntityType.Course, "courseId")]
        public IEnumerable<CourseTestDetailsVM> GetNonPublishedTests(string courseId)
        {
            return GetAndFilterTests(courseId, courseTestFilter.FilterNonPublishedBeforeDeadline);
        }

        /// <summary>
        /// get all tests in the given course that are after deadline
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{courseId}/testsAfterDeadline")]
        [AuthorizeCourseAdminOf(EntityType.Course, "courseId")]
        public IEnumerable<CourseTestDetailsVM> GetTestsAfterDeadline(string courseId)
        {
            return GetAndFilterTests(courseId, courseTestFilter.FilterAfterDeadline);
        }

        /// <summary>
        /// get all posts in the course with given id
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        [HttpGet("{courseId}/posts")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.Course, "courseId")]
        public IEnumerable<ForumPostVM> GetAllPosts(string courseId)
        {
            var posts = courseService.GetPostsWithAuthors(courseId);
            return posts.Select(post => new ForumPostVM(post.Id.ToString(), post.Author.Email, post.Text));
        }

        /// <summary>
        /// get all requests for enrollments to this course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("{courseId}/enrollmentRequests")]
        [AuthorizeCourseAdminOf(EntityType.Course, "courseId")]
        public IEnumerable<EnrollmentRequestVM> GetEnrollmentRequests(string courseId)
        {
            var requests = courseService.GetEnrollmentRequestsWithPeople(courseId);
            return requests.Select(req => new EnrollmentRequestVM(req.Id.ToString(), req.Person.UserName, req.Person.Email));
        }

        /// <summary>
        /// remove the current course member from the selected course
        /// </summary>
        /// <param name="courseId">idnetifier of the given course</param>
        [HttpDelete("{courseId}/removeCurrentMember")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.Course, "courseId")]
        public void RemoveCurrentMember(string courseId)
        {
            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
            var currentMember = courseMemberService.GetMemberByUserAndCourse(currentUserId, courseId);
            courseMemberService.ArchiveMemberById(currentMember.Id.ToString());
            courseMemberService.CommitChanges();
        }

        /// <summary>
        /// remove the current course admin from the selected course
        /// <br/>
        /// if there is just 1 admin, throw <see cref="ArgumentException"/> 
        /// </summary>
        /// <param name="courseId">idnetifier of the given course</param>
        [HttpDelete("{courseId}/removeCurrentAdmin")]
        [AuthorizeCourseAdminOf(EntityType.Course, "courseId")]
        public void RemoveCurrentAdmin(string courseId)
        {
            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
            var currentAdmin = courseAdminService.GetByUserAndCourse(currentUserId, courseId);

            var adminCount = courseService.GetAdminsWithUsers(courseId).Count;
            if (adminCount >= 2)
            {
                courseAdminService.RemoveById(currentAdmin.Id.ToString());
                courseAdminService.CommitChanges();
            }
            else
            {
                throw new ArgumentException("Cannot remove the last admin of the course.");
            }
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