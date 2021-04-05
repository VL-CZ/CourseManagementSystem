using CourseManagementSystem.Data;
using CourseManagementSystem.Services.Interfaces;

namespace CourseManagementSystem.Services.Implementations
{
    public class GradeService : DbService, IGradeService
    {
        public GradeService(CMSDbContext dbContext):base(dbContext)
        {
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