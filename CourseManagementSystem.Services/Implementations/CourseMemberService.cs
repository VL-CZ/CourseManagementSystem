using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Services
{
    public class CourseMemberService : ICourseMemberService
    {
        private CMSDbContext dbContext;

        public CourseMemberService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public CourseMember GetMemberByID(int id)
        {
            return dbContext.CourseMembers.Include(cm => cm.User).Include(cm => cm.Grades).Single(x => x.Id == id);
        }

        /// <inheritdoc/>
        public CourseMember GetMemberByUserAndCourse(string personId, int courseId)
        {
            Person user = dbContext.Users.Include(u => u.CourseMemberships).ThenInclude(cm => cm.Course).SingleOrDefault(user => user.Id == personId);
            return user.CourseMemberships.SingleOrDefault(cm => cm.Course.Id == courseId);
        }

        /// <inheritdoc/>
        public void RemoveMemberById(int id)
        {
            CourseMember cm = GetMemberByID(id);
            dbContext.CourseMembers.Remove(cm);
            dbContext.SaveChanges();
        }
    }
}