using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class CourseTestService : ICourseTestService
    {
        private readonly CMSDbContext dbContext;

        public CourseTestService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void AddToCourse(CourseTest test, int courseId)
        {
            var course = dbContext.Courses.Find(courseId);
            course.Tests.Add(test);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void Delete(int testId)
        {
            var testToRemove = GetById(testId);
            dbContext.CourseTests.Remove(testToRemove);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public CourseTest GetById(int testId)
        {
            return dbContext.CourseTests.Include(x => x.Course).Include(ct => ct.Questions).SingleOrDefault(ct => ct.Id == testId);
        }
    }
}