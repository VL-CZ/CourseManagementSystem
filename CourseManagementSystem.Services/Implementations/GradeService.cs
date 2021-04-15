using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
using CourseManagementSystem.Services.Interfaces;

namespace CourseManagementSystem.Services.Implementations
{
    public class GradeService : DbService, IGradeService
    {
        public GradeService(CMSDbContext dbContext) : base(dbContext)
        {
        }

        /// <inheritdoc/>
        public void DeleteById(string gradeId)
        {
            var grade = GetById(gradeId);
            dbContext.Grades.Remove(grade);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public string GetCourseIdOf(string objectId)
        {
            string courseMemberId = dbContext.Grades.GetCourseMemberIdOf(objectId);
            return dbContext.CourseMembers.GetCourseIdOf(courseMemberId);
        }

        /// <summary>
        /// get grade by its id
        /// </summary>
        /// <param name="gradeId">identifier of the grade</param>
        /// <returns></returns>
        private Grade GetById(string gradeId)
        {
            return dbContext.Grades.FindById(gradeId);
        }
    }
}