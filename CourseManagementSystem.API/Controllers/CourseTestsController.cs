using CourseManagementSystem.API.Auth;
using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services;
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
    public class CourseTestsController : ControllerBase
    {
        private readonly ICourseTestService courseTestService;
        private readonly ITestSubmissionService testSubmissionService;

        public CourseTestsController(ICourseTestService courseTestService, ITestSubmissionService testSubmissionService)
        {
            this.courseTestService = courseTestService;
            this.testSubmissionService = testSubmissionService;
        }

        /// <summary>
        /// add new test to the given course
        /// </summary>
        /// <param name="testToAdd"></param>
        /// <param name="courseId"></param>
        [HttpPost("{courseId}")]
        public void Add(AddCourseTestVM testToAdd, string courseId)
        {
            var mappedQuestions = testToAdd.Questions.ToModels();
            var test = new CourseTest(testToAdd.Topic, mappedQuestions.ToList(), testToAdd.Weight, testToAdd.Deadline);
            courseTestService.AddToCourse(test, courseId);
        }

        /// <summary>
        /// delete test by its id
        /// </summary>
        /// <param name="testId"></param>
        [HttpDelete("{testId}")]
        [Authorize(Policy = CourseAdminRequirement.policyName)]
        public void Delete(string testId)
        {
            courseTestService.Delete(testId);
        }

        /// <summary>
        /// get test by Id
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        [HttpGet("{testId}")]
        [Authorize(Policy = CourseAdminRequirement.policyName)]
        public CourseTestDetailsVM Get(string testId)
        {
            var test = courseTestService.GetById(testId);
            return new CourseTestDetailsVM(testId, test.Topic, test.Weight, test.Questions.ToViewModels(), test.Status, test.Deadline);
        }

        /// <summary>
        /// update properties (including questions) of the test
        /// </summary>
        /// <param name="testId">id of the test that we edit</param>
        /// <param name="updatedTest">test with updated properties</param>
        [HttpPut("{testId}")]
        [Authorize(Policy = CourseAdminRequirement.policyName)]
        public void Update(string testId, AddCourseTestVM updatedTest)
        {
            var test = courseTestService.GetById(testId);
            var updatedQuestions = updatedTest.Questions.ToModels();
            courseTestService.Update(test, updatedTest.Weight, updatedTest.Topic, updatedTest.Deadline, updatedQuestions.ToList());
        }

        /// <summary>
        /// get info about all test submissions submitted to the given test
        /// </summary>
        /// <param name="testId">id of the test</param>
        /// <returns>collection of submissions</returns>
        [HttpGet("{testId}/submissions")]
        [Authorize(Policy = CourseAdminRequirement.policyName)]
        public IEnumerable<TestSubmissionWithUserInfoVM> GetAllSubmissions(string testId)
        {
            var testSubmissions = testSubmissionService.GetAllSubmissionsOfTest(testId);
            return testSubmissions.Select(ts => new TestSubmissionWithUserInfoVM(ts.Student.User.Email, ts.Id.ToString(),
                TestScoreCalculator.CalculateScore(ts), ts.SubmittedDateTime, ts.IsReviewed));
        }

        /// <summary>
        /// publish the test with given id
        /// </summary>
        /// <param name="testId">identifier of the test</param>
        [HttpPost("{testId}/publish")]
        [Authorize(Policy = CourseAdminRequirement.policyName)]
        public void Publish(string testId)
        {
            var test = courseTestService.GetById(testId);
            courseTestService.Publish(test);
        }
    }
}