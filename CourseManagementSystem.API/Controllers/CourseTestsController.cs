using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            courseTestService.Delete(id);
        }

        /// <summary>
        /// get test by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public CourseTestDetailsVM Get(string id)
        {
            var test = courseTestService.GetById(id);
            return new CourseTestDetailsVM(id, test.Topic, test.Weight, test.Questions.ToViewModels(), test.Status, test.Deadline);
        }

        /// <summary>
        /// update properties (including questions) of the test
        /// </summary>
        /// <param name="id">id of the test that we edit</param>
        /// <param name="updatedTest">test with updated properties</param>
        [HttpPut("{id}")]
        public void Update(string id, AddCourseTestVM updatedTest)
        {
            var test = courseTestService.GetById(id);
            var updatedQuestions = updatedTest.Questions.ToModels();
            courseTestService.Update(test, updatedTest.Weight, updatedTest.Topic, updatedTest.Deadline, updatedQuestions.ToList());
        }

        /// <summary>
        /// get info about all test submissions submitted to the given test
        /// </summary>
        /// <param name="testId">id of the test</param>
        /// <returns>collection of submissions</returns>
        [HttpGet("{testId}/submissions")]
        public IEnumerable<TestSubmissionWithUserInfoVM> GetAllSubmissions(string testId)
        {
            var testSubmissions = testSubmissionService.GetAllSubmissionsOfTest(testId);
            return testSubmissions.Select(ts => new TestSubmissionWithUserInfoVM(ts.Student.User.Email, ts.Id.ToString(),
                TestScoreCalculator.CalculateScore(ts), ts.SubmittedDateTime, ts.IsReviewed));
        }

        /// <summary>
        /// publish the test with given id
        /// </summary>
        /// <param name="id">identifier of the test</param>
        [HttpPost("{id}/publish")]
        public void Publish(string id)
        {
            var test = courseTestService.GetById(id);
            courseTestService.Publish(test);
        }
    }
}