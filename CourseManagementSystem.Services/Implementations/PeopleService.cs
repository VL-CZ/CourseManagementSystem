using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class PeopleService : DbService, IPeopleService
    {
        public PeopleService(CMSDbContext dbContext) : base(dbContext)
        {
        }

        /// <inheritdoc/>
        public void EnrollTo(Person person, Course course)
        {
            var cm = new CourseMember(person, course);
            dbContext.CourseMembers.Add(cm);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public Person GetById(string personId)
        {
            return dbContext.Users.Find(personId);
        }

        /// <inheritdoc/>
        public CourseMember GetCourseMembership(Person person, Course course)
        {
            return dbContext.CourseMembers.Include(cm => cm.Course).Include(cm => cm.User)
                .Where(cm => cm.User.Id == person.Id)
                .Where(cm => cm.Course.Id == course.Id)
                .SingleOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<Course> GetManagedCourses(string personId)
        {
            return dbContext.Courses.Include(c => c.Admin).Where(c => c.Admin.Id == personId);
        }

        /// <inheritdoc/>
        public IEnumerable<Course> GetMemberCourses(string personId)
        {
            return dbContext.CourseMembers.Include(cm => cm.Course).Include(cm => cm.User)
                .Where(cm => cm.User.Id == personId).Select(cm => cm.Course);
        }
    }
}