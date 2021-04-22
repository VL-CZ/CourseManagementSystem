using CourseManagementSystem.API.Auth;
using CourseManagementSystem.API.Auth.Attributes;
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
        public CourseMemberVM Get(string id)
        {
            CourseMember cm = courseMemberService.GetMemberWithUser(id);
            return new CourseMemberVM(cm.User.Id, cm.User.UserName, cm.User.Email);
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

            courseMemberService.CommitChanges();
        }

        /// <summary>
        /// get all test submissions of this <see cref="CourseMember"/>
        /// </summary>
        /// <param name="id">ID of the <see cref="CourseMember"/></param>
        /// <returns>all test submissions of the course member</returns>
        [HttpGet("{id}/submissions")]
        [AuthorizeCourseAdminOrOwnerOf(EntityType.CourseMember, "id")]
        public IEnumerable<TestSubmissionInfoVM> GetTestSubmissions(string id)
        {
            var userSubmissions = testSubmissionService.GetAllSubmissionsOfCourseMember(id);
            return userSubmissions.Select(ts => new TestSubmissionInfoVM(ts.Id.ToString(), ts.Test.Topic, ts.Test.Weight,
                TestScoreCalculator.CalculateScore(ts), ts.SubmittedDateTime, ts.IsReviewed));
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
            var courseMember = courseMemberService.GetMemberWithUser(id);
            return courseMember.Grades.Select(grade => 
                new GradeDetailsVM(grade.Id.ToString(), grade.PercentualValue, grade.Topic, grade.Comment, grade.Weight));
        }
    }
}