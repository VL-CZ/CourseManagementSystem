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
        public void Add(CourseTestVM testToAdd, int courseId)
        {
            var mappedQuestions = testToAdd.Questions.Select(q => new TestQuestion(q.Number, q.QuestionText, q.CorrectAnswer, q.Points));
            var test = new CourseTest(testToAdd.Topic, mappedQuestions.ToList(), testToAdd.Weight);
            courseTestService.AddToCourse(test, courseId);
        }

        /// <summary>
        /// delete test by its id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            courseTestService.Delete(id);
        }

        /// <summary>
        /// get test by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public CourseTestVM Get(int id)
        {
            var test = courseTestService.GetById(id);
            return new CourseTestVM(id, test.Topic, test.Weight, test.Questions.ToViewModels());
        }

        /// <summary>
        /// get info about all test submissions submitted to the given test
        /// </summary>
        /// <param name="testId">id of the test</param>
        /// <returns>collection of submissions</returns>
        [HttpGet("{testId}/submissions")]
        public IEnumerable<TestSubmissionWithUserInfoVM> GetAllSubmissions(int testId)
        {
            var testSubmissions = testSubmissionService.GetAllSubmissionsOfTest(testId);
            return testSubmissions.Select(ts => new TestSubmissionWithUserInfoVM(ts.Student.User.Email, ts.Id, TestScoreCalculator.CalculateScore(ts)));
        }
    }
}