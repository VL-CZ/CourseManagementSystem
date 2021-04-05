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
    public class CourseMembersController : ControllerBase
    {
        private readonly ICourseMemberService courseMemberService;
        private readonly ITestSubmissionService testSubmissionService;

        public CourseMembersController(ICourseMemberService courseMemberService, ITestSubmissionService testSubmissionService)
        {
            this.courseMemberService = courseMemberService;
            this.testSubmissionService = testSubmissionService;
        }

        /// <summary>
        /// get person by id
        /// </summary>
        /// <param name="id">identifier of the person</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public CourseMemberVM Get(int id)
        {
            CourseMember cm = courseMemberService.GetMemberByID(id);
            return new CourseMemberVM(cm.User.Id, cm.User.UserName, cm.User.Email);
        }

        /// <summary>
        /// assign grade to the person with selected id
        /// </summary>
        /// <param name="id">identifier of the <see cref="CourseMember"/></param>
        /// <param name="g">grade viewmodel to add</param>
        /// <returns>assigned grade</returns>
        [HttpPost("{id}/assignGrade")]
        public void AssignGrade(int id, AddGradeVM g)
        {
            CourseMember cm = courseMemberService.GetMemberByID(id);
            Grade grade = new Grade(g.PercentualValue, g.Comment, g.Topic, g.Weight);
            courseMemberService.AssignGrade(cm, grade);
        }

        /// <summary>
        /// get all test submissions of this <see cref="CourseMember"/>
        /// </summary>
        /// <param name="id">ID of the <see cref="CourseMember"/></param>
        /// <returns>all test submissions of the course member</returns>
        [HttpGet("{id}/submissions")]
        public IEnumerable<TestSubmissionInfoVM> GetTestSubmissions(int id)
        {
            var userSubmissions = testSubmissionService.GetAllSubmissionsOfCourseMember(id);
            return userSubmissions.Select(ts => new TestSubmissionInfoVM(ts.Id, ts.Test.Topic, ts.Test.Weight, TestScoreCalculator.CalculateScore(ts)));
        }

        /// <summary>
        /// get all grades of this <see cref="CourseMember"/>
        /// </summary>
        /// <param name="id">ID of the <see cref="CourseMember"/></param>
        /// <returns>all grades (excluding test submissions) of the course member</returns>
        [HttpGet("{id}/grades")]
        public IEnumerable<GradeDetailsVM> GetGrades(int id)
        {
            var courseMember = courseMemberService.GetMemberByID(id);
            return courseMember.Grades.Select(g => new GradeDetailsVM(g.Id, g.PercentualValue, g.Topic, g.Comment, g.Weight));
        }
    }
}