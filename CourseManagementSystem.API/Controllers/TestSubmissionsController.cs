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
        /// <param name="testSubmissionVM">solution to submit</param>
        [HttpPost("")]
        public void Submit(TestSubmissionVM testSubmissionVM)
        {
            var test = courseTestService.GetById(testSubmissionVM.TestId);

            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
            var courseMember = courseMemberService.GetMemberByUserAndCourse(currentUserId, test.Course.Id);

            var testSubmission = new TestSubmission(test, courseMember,
                testSubmissionVM.Answers.Select(answer => new TestSubmissionAnswer(test.GetQuestionByNumber(answer.QuestionNumber), answer.AnswerText)).ToList());

            dbContext.SaveChanges();
        }

        /// <summary>
        /// get new empty test submission
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        [HttpGet("{testId}")]
        public TestSubmissionVM GetEmptySubmission(int testId)
        {
            var test = courseTestService.GetById(testId);
            var submissionAnswers = test.Questions.Select(question => new SubmissionAnswerVM(question.Number, question.QuestionText, string.Empty));

            return new TestSubmissionVM(test.Id, test.Topic, submissionAnswers);
        }
    }
}