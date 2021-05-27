using CourseManagementSystem.API.Auth;
using CourseManagementSystem.API.Auth.Attributes;
using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using CourseManagementSystem.TestEvaluation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestSubmissionsController : ControllerBase
    {
        private readonly ICourseTestService courseTestService;
        private readonly ICourseMemberService courseMemberService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITestSubmissionService testSubmissionService;
        private readonly TestSubmissionEvaluator testSubmissionEvaluator;

        public TestSubmissionsController(ICourseTestService courseTestService, ICourseMemberService courseMemberService, IHttpContextAccessor httpContextAccessor,
            ITestSubmissionService testSubmissionService)
        {
            this.courseTestService = courseTestService;
            this.courseMemberService = courseMemberService;
            this.httpContextAccessor = httpContextAccessor;
            this.testSubmissionService = testSubmissionService;
            testSubmissionEvaluator = new TestSubmissionEvaluator();
        }

        /// <summary>
        /// submit a solution
        /// </summary>
        /// <param name="testSubmissionId">identifier of the <see cref="TestSubmission"/> to submit</param>
        [HttpPost("{testSubmissionId}/submit")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.CourseTest, "testSubmissionId")]
        public void Submit(string testSubmissionId)
        {
            var testSubmission = testSubmissionService.GetSubmissionWithTestAndQuestions(testSubmissionId);

            testSubmissionService.Submit(testSubmission);
            testSubmissionEvaluator.Evaluate(testSubmission);

            testSubmissionService.CommitChanges();
        }

        /// <summary>
        /// save answers to a test submission
        /// </summary>
        /// <param name="testSubmissionId">identifier of the <see cref="TestSubmission"/> to save</param>
        /// <param name="updatedAnswers">updated contents of the answers</param>
        [HttpPut("{testSubmissionId}/save")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.CourseTest, "testSubmissionId")]
        public void SaveAnswers(string testSubmissionId, IEnumerable<SubmissionAnswerVM> updatedAnswers)
        {
            var testSubmission = testSubmissionService.GetSubmissionWithTestAndQuestions(testSubmissionId);
            
            foreach (var answer in updatedAnswers)
            {
                testSubmissionService.UpdateAnswerText(testSubmission, answer.QuestionNumber, answer.AnswerText);
            }

            testSubmissionService.CommitChanges();
        }

        /// <summary>
        /// load existing test submission
        /// if it doesn't exist get new empty submission
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        [HttpPost("load/{testId}")]
        [AuthorizeCourseAdminOrMemberOf(EntityType.CourseTest, "testId")]
        public SubmitTestVM LoadTestSubmission(string testId)
        {
            var test = courseTestService.GetWithQuestions(testId);
            string courseId = courseTestService.GetCourseIdOf(testId);

            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
            var courseMember = courseMemberService.GetMemberByUserAndCourse(currentUserId, courseId);

            var foundTestSubmission = testSubmissionService.LoadOrCreateSubmission(test, courseMember);

            var answersVM = foundTestSubmission.Answers.Select(answer => new SubmissionAnswerVM(answer.Question.Number, answer.Question.QuestionText, answer.Text));

            testSubmissionService.CommitChanges();
            return new SubmitTestVM(foundTestSubmission.Id.ToString(), test.Topic, answersVM);
        }

        /// <summary>
        /// get test submission by id
        /// </summary>
        /// <param name="testSubmissionId">id of the submission</param>
        /// <returns>test submission with the given id</returns>
        [HttpGet("{testSubmissionId}")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.TestSubmission, "testSubmissionId")]
        public TestWithSubmissionVM GetTestSubmission(string testSubmissionId)
        {
            TestSubmission submission = testSubmissionService.GetSubmissionWithTestAndQuestions(testSubmissionId);
            var answersVM = submission.Answers.Select(a =>
                new SubmissionAnswerWithCorrectAnswerVM(a.Question.Number, a.Question.QuestionText, a.Text, a.Question.CorrectAnswer, a.Points, a.Question.Points, a.Comment));

            return new TestWithSubmissionVM(submission.Test.Id.ToString(), submission.Test.Topic, submission.Id.ToString(), answersVM, submission.SubmittedDateTime, submission.IsReviewed);
        }

        /// <summary>
        /// update test submission properties - points and comments
        /// </summary>
        /// <param name="testSubmissionId">id of the submission that is evaluated</param>
        /// <param name="evaluatedTestSubmission">the evaluated test submission</param>
        [HttpPut("{testSubmissionId}")]
        [AuthorizeCourseAdminOf(EntityType.TestSubmission, "testSubmissionId")]
        public void UpdateTestSubmission(string testSubmissionId, EvaluatedTestSubmissionVM evaluatedTestSubmission)
        {
            TestSubmission submission = testSubmissionService.GetSubmissionWithTestAndQuestions(testSubmissionId);
            foreach (var evaluatedAnswer in evaluatedTestSubmission.EvaluatedAnswers)
            {
                var answer = testSubmissionService.GetAnswerByQuestionNumber(submission, evaluatedAnswer.QuestionNumber);
                testSubmissionService.UpdateAnswer(answer, evaluatedAnswer.UpdatedPoints, evaluatedAnswer.UpdatedComment);
            }

            testSubmissionService.MarkAsReviewed(submission);

            testSubmissionService.CommitChanges();
        }
    }
}