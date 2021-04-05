using CourseManagementSystem.Data;
using CourseManagementSystem.Services.Interfaces;

namespace CourseManagementSystem.Services.Implementations
{
    public class GradeService : IGradeService
    {
        private CMSDbContext dbContext;

        public GradeService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void DeleteById(int gradeId)
        {
            var grade = dbContext.Grades.Find(gradeId);
            dbContext.Grades.Remove(grade);
            dbContext.SaveChanges();
        }
    }
}