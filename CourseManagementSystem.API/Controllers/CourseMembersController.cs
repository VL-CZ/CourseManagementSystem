using CourseManagementSystem.API.Services;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
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
        private readonly CMSDbContext dbContext;
        private readonly ICourseMemberService courseMemberService;
        private readonly ITestSubmissionService testSubmissionService;

        public CourseMembersController(CMSDbContext dbContext, ICourseMemberService courseMemberService, ITestSubmissionService testSubmissionService)
        {
            this.dbContext = dbContext;
            this.courseMemberService = courseMemberService;
            this.testSubmissionService = testSubmissionService;
        }

        /// <summary>
        /// get person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public StudentVM Get(int id)
        {
            CourseMember cm = courseMemberService.GetMemberByID(id);

            return new StudentVM
            {
                Email = cm.User.Email,
                Id = cm.User.Id,
                Name = cm.User.UserName,
                Grades = cm.Grades.Select(g => new GradeDetailsVM(g.Id, g.PercentualValue, g.Topic, g.Comment, g.Weight))
            };
        }

        /// <summary>
        /// assign grade to the person with selected id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="g"></param>
        /// <returns>assigned grade</returns>
        [HttpPost("{id}/assignGrade")]
        public GradeDetailsVM AssignGrade(int id, [FromBody] AddGradeVM g)
        {
            CourseMember cm = courseMemberService.GetMemberByID(id);
            Grade grade = new Grade { PercentualValue = g.PercentualValue, Comment = g.Comment, Topic = g.Topic };
            cm.AssignGrade(grade);
            dbContext.SaveChanges();

            return new GradeDetailsVM(grade.Id, grade.PercentualValue, grade.Topic, grade.Comment, grade.Weight);
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
    }
}