using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.Services;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSubmissionsController : ControllerBase
    {
        private readonly ICourseTestService courseTestService;
        private readonly ICourseMemberService courseMemberService;
        private IHttpContextAccessor httpContextAccessor;
        private readonly CMSDbContext dbContext;

        public TestSubmissionsController(ICourseTestService courseTestService, ICourseMemberService courseMemberService, CMSDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.courseTestService = courseTestService;
            this.courseMemberService = courseMemberService;
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// submit a solution to the given test
        /// </summary>
        /// <param name="submission">solution to submit</param>
        /// <param name="testId"></param>
        [HttpPost("{testId}")]
        public void Submit(TestSubmissionVM submissionVM, int testId)
        {
            var test = courseTestService.GetById(testId);
            var answers = submissionVM.Answers.Select(answer => new TestSubmissionAnswer(test.GetQuestionByNumber(answer.QuestionNumber), answer.Text));
            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId(); // TO-DO get courseMember by its ID from httpcontext


            var submission = new TestSubmission(test, null, answers.ToList());

            test.Submissions.Add(submission);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// get given test submission
        /// </summary>
        /// <param name="testSubmissionId"></param>
        /// <returns></returns>
        [HttpGet("{testSubmissionId}")]
        public TestSubmission Get(int testSubmissionId)
        {
            return null;
        }
    }
}