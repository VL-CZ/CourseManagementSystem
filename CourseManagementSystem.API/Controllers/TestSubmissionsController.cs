﻿using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
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
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITestSubmissionService testSubmissionService;
        private readonly ITestSubmissionEvaluator testSubmissionEvaluator;

        public TestSubmissionsController(ICourseTestService courseTestService, ICourseMemberService courseMemberService, IHttpContextAccessor httpContextAccessor,
            ITestSubmissionService testSubmissionService, ITestSubmissionEvaluator testSubmissionEvaluator)
        {
            this.courseTestService = courseTestService;
            this.courseMemberService = courseMemberService;
            this.httpContextAccessor = httpContextAccessor;
            this.testSubmissionService = testSubmissionService;
            this.testSubmissionEvaluator = testSubmissionEvaluator;
        }

        /// <summary>
        /// submit a solution to the given test
        /// </summary>
        /// <param name="testSubmissionVM">solution to submit</param>
        /// <returns>Id of the test submission</returns>
        [HttpPost("")]
        public int Submit(SubmitTestVM testSubmissionVM)
        {
            var test = courseTestService.GetById(testSubmissionVM.TestId);

            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
            var courseMember = courseMemberService.GetMemberByUserAndCourse(currentUserId, test.Course.Id);

            var testSubmission = new TestSubmission(test, courseMember,
                testSubmissionVM.Answers.Select(answer => new TestSubmissionAnswer(test.GetQuestionByNumber(answer.QuestionNumber), answer.AnswerText)).ToList());

            testSubmissionEvaluator.Evaluate(testSubmission);
            testSubmissionService.Save(testSubmission);

            return testSubmission.Id;
        }

        /// <summary>
        /// get new empty test submission
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        [HttpGet("emptyTest/{testId}")]
        public SubmitTestVM GetEmptySubmission(int testId)
        {
            var test = courseTestService.GetById(testId);
            var submissionAnswers = test.Questions.Select(question => new SubmissionAnswerVM(question.Number, question.QuestionText, string.Empty));

            return new SubmitTestVM(test.Id, test.Topic, submissionAnswers);
        }

        /// <summary>
        /// get test submission by id
        /// </summary>
        /// <param name="testSubmissionId">id of the submission</param>
        /// <returns>test submission with the given id</returns>
        [HttpGet("{testSubmissionId}")]
        public TestWithSubmissionVM GetTestSubmission(int testSubmissionId)
        {
            TestSubmission submission = testSubmissionService.GetSubmissionById(testSubmissionId);
            var answersVM = submission.Answers.Select(a =>
                new SubmissionAnswerWithCorrectAnswerVM(a.Question.Number, a.Question.QuestionText, a.Text, a.Question.CorrectAnswer, a.Points, a.Question.Points, a.Comment));

            return new TestWithSubmissionVM(submission.Test.Id, submission.Test.Topic, submission.Id, answersVM);
        }

        /// <summary>
        /// update test submission properties - points and comments
        /// </summary>
        /// <param name="testSubmissionId">id of the submission that is evaluated</param>
        /// <param name="evaluatedTestSubmission">the evaluated test submission</param>
        [HttpPut("{testSubmissionId}")]
        public void UpdateTestSubmission(int testSubmissionId, EvaluatedTestSubmissionVM evaluatedTestSubmission)
        {
            TestSubmission submission = testSubmissionService.GetSubmissionById(testSubmissionId);
            foreach (var evaluatedAnswer in evaluatedTestSubmission.EvaluatedAnswers)
            {
                var answer = testSubmissionService.GetAnswerByQuestionNumber(submission, evaluatedAnswer.QuestionNumber);
                testSubmissionService.UpdateAnswer(answer, evaluatedAnswer.UpdatedPoints, evaluatedAnswer.UpdatedComment);
            }

            testSubmissionService.MarkAsReviewed(submission);
        }
    }
}