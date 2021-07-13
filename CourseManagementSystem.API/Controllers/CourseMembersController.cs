using CourseManagementSystem.API.Auth;
using CourseManagementSystem.API.Auth.Attributes;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using CourseManagementSystem.TestEvaluation.Calculators;
using CourseManagementSystem.TestEvaluation.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseMembersController : ControllerBase
    {
        private readonly ICourseMemberService courseMemberService;
        private readonly IGradeService gradeService;
        private readonly ITestSubmissionService testSubmissionService;

        public CourseMembersController(ICourseMemberService courseMemberService, ITestSubmissionService testSubmissionService, IGradeService gradeService)
        {
            this.courseMemberService = courseMemberService;
            this.testSubmissionService = testSubmissionService;
            this.gradeService = gradeService;
        }

        /// <summary>
        /// get course member by its id
        /// </summary>
        /// <param name="id">identifier of the course member</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.CourseMember, "id")]
        public CourseMemberOrAdminVM Get(string id)
        {
            CourseMember cm = courseMemberService.GetMemberWithUser(id);
            return new CourseMemberOrAdminVM(cm.User.Id, cm.User.UserName, cm.User.Email);
        }

        /// <summary>
        /// assign grade to the course member with selected id
        /// </summary>
        /// <param name="id">identifier of the <see cref="CourseMember"/></param>
        /// <param name="gradeVM">grade viewmodel to add</param>
        /// <returns>assigned grade</returns>
        [HttpPost("{id}/assignGrade")]
        [AuthorizeCourseAdminOf(EntityType.CourseMember, "id")]
        public void AssignGrade(string id, AddGradeVM gradeVM)
        {
            CourseMember cm = courseMemberService.GetMemberWithUser(id);
            Grade grade = new Grade(gradeVM.PercentualValue, gradeVM.Comment, gradeVM.Topic, gradeVM.Weight, cm);

            gradeService.AssignGrade(grade);

            gradeService.CommitChanges();
        }

        /// <summary>
        /// archive course member by its id
        /// </summary>
        /// <param name="id">identifier of the course member</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [AuthorizeCourseAdminOf(EntityType.CourseMember, "id")]
        public void Archive(string id)
        {
            courseMemberService.ArchiveMemberById(id);
            courseMemberService.CommitChanges();
        }

        /// <summary>
        /// get all graded test submissions of this <see cref="CourseMember"/>
        /// </summary>
        /// <param name="id">ID of the <see cref="CourseMember"/></param>
        /// <returns>all test submissions of the course member</returns>
        [HttpGet("{id}/submissions")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.CourseMember, "id")]
        public IEnumerable<TestSubmissionInfoVM> GetTestSubmissions(string id)
        {
            var userSubmissions = testSubmissionService.GetAllGraded(id);
            return userSubmissions.Select(ts => new TestSubmissionInfoVM(ts.Id.ToString(), ts.Test.Topic, ts.Test.Weight,
                TestScoreCalculator.CalculateScore(ts), ts.SubmittedDateTime, ts.IsReviewed));
        }

        /// <summary>
        /// get all quizzes (non-graded test submissions) of the <see cref="CourseMember"/>
        /// </summary>
        /// <param name="id">ID of the <see cref="CourseMember"/></param>
        /// <returns>all test submissions of the course member</returns>
        [HttpGet("{id}/quizSubmissions")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.CourseMember, "id")]
        public IEnumerable<QuizSubmissionInfoVM> GetQuizzes(string id)
        {
            var userSubmissions = testSubmissionService.GetAllQuizzes(id);
            return userSubmissions.Select(quiz => new QuizSubmissionInfoVM(quiz.Id.ToString(), quiz.Test.Topic));
        }

        /// <summary>
        /// get all grades of this <see cref="CourseMember"/>
        /// </summary>
        /// <param name="id">ID of the <see cref="CourseMember"/></param>
        /// <returns>all grades (excluding test submissions) of the course member</returns>
        [HttpGet("{id}/grades")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.CourseMember, "id")]
        public IEnumerable<GradeDetailsVM> GetGrades(string id)
        {
            var grades = courseMemberService.GetGradesOf(id);
            return grades.Select(grade =>
                new GradeDetailsVM(grade.Id.ToString(), grade.PercentualValue, grade.Topic, grade.Comment, grade.Weight));
        }

        /// <summary>
        /// get average score of this <see cref="CourseMember"/>
        /// </summary>
        /// <param name="id">ID of the <see cref="CourseMember"/></param>
        /// <returns>average score from test submissions and grades of this student (0=0%,1=100%)</returns>
        [HttpGet("{id}/averageScore")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.CourseMember, "id")]
        public WrapperVM<double> GetAverageScore(string id)
        {
            var mappedGrades = courseMemberService.GetGradesOf(id)
                .Select(grade => new ScoreWithWeightDto(grade.Weight, grade.PercentualValue));
            var mappedTestSubmissions = testSubmissionService.GetAllGraded(id)
                .Select(ts => new ScoreWithWeightDto(ts.Test.Weight, TestScoreCalculator.CalculateScore(ts)));

            var scoresWithWeights = new List<ScoreWithWeightDto>();
            scoresWithWeights.AddRange(mappedGrades);
            scoresWithWeights.AddRange(mappedTestSubmissions);

            double averageScore = AverageScoreCalculator.GetScore(scoresWithWeights);
            return new WrapperVM<double>(averageScore);
        }

        /// <summary>
        /// get id of the course that this course member entity belongs to
        /// </summary>
        /// <param name="id">identifier of the course member</param>
        /// <returns></returns>
        [HttpGet("{id}/courseId")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.CourseMember, "id")]
        public WrapperVM<string> GetCourseId(string id)
        {
            string courseId = courseMemberService.GetCourseIdOf(id);
            return new WrapperVM<string>(courseId);
        }
    }
}