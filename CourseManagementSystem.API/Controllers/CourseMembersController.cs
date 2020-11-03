using CourseManagementSystem.API.Services;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseMembersController : ControllerBase
    {
        private readonly CMSDbContext dbContext;
        private readonly ICourseMemberService courseMemberService;

        public CourseMembersController(CMSDbContext dbContext, ICourseMemberService courseMemberService)
        {
            this.dbContext = dbContext;
            this.courseMemberService = courseMemberService;
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
                Id = cm.User.ToString(),
                Name = cm.User.UserName,
                Grades = cm.Grades.Select(g => new GradeDetailsVM() { Id = g.ID, Comment = g.Comment, Topic = g.Topic, Value = g.Value })
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
            Grade grade = new Grade { Value = g.Value, Comment = g.Comment, Topic = g.Topic };
            cm.AssignGrade(grade);
            dbContext.SaveChanges();

            return new GradeDetailsVM() { Id = grade.ID, Comment = grade.Comment, Value = grade.Value, Topic = grade.Topic };
        }
    }
}