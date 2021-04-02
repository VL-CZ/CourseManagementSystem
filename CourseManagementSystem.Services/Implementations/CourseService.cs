using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;

namespace CourseManagementSystem.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly CMSDbContext dbContext;

        public CourseService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public Course GetById(int courseId)
        {
            return dbContext.Courses.Find(courseId);
        }
    }
}