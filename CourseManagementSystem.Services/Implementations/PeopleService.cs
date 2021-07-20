using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
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
        public IEnumerable<Course> GetActiveManagedCourses(string personId)
        {
            return GetManagedCourses(personId).FilterActive();
        }

        /// <inheritdoc/>
        public IEnumerable<Course> GetActiveMemberCourses(string personId)
        {
            return GetMemberCourses(personId).FilterActive();
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
                .Where(cm => !cm.IsArchived)
                .Single();
        }

        /// <inheritdoc/>
        public bool IsAdminOfCourse(string personId, string courseId)
        {
            return GetManagedCourses(personId).Any(course => course.Id.ToString() == courseId);
        }

        /// <inheritdoc/>
        public bool IsMemberOfCourse(string personId, string courseId)
        {
            return GetMemberCourses(personId).Any(course => course.Id.ToString() == courseId);
        }

        /// <summary>
        /// get managed courses of the person (e.g. all courses where the person is an admin)
        /// </summary>
        /// <param name="personId">id of the person</param>
        /// <returns></returns>
        private IEnumerable<Course> GetManagedCourses(string personId)
        {
            return dbContext.CourseAdmins
                .Include(admin => admin.User)
                .Include(admin => admin.Course)
                .Where(admin => admin.User.Id == personId)
                .Select(admin => admin.Course);
        }

        /// <summary>
        /// get all courses whose member the given person is
        /// </summary>
        /// <param name="personId">id of the person</param>
        /// <returns></returns>
        private IEnumerable<Course> GetMemberCourses(string personId)
        {
            return dbContext.CourseMembers
                .Include(cm => cm.Course)
                .Include(cm => cm.User)
                .Where(cm => cm.User.Id == personId)
                .Where(cm => !cm.IsArchived)
                .Select(cm => cm.Course);
        }
    }
}