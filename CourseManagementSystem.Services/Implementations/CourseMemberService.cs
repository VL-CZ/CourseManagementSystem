using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class CourseMemberService : DbService, ICourseMemberService
    {
        public CourseMemberService(CMSDbContext dbContext) : base(dbContext)
        { }

        /// <inheritdoc/>
        public void AssignGrade(CourseMember courseMember, Grade grade)
        {
            courseMember.Grades.Add(grade);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public CourseMember GetMemberByID(string id)
        {
            return dbContext.CourseMembers.Include(cm => cm.User).Include(cm => cm.Grades).Single(cm => cm.Id.ToString() == id);
        }

        /// <inheritdoc/>
        public CourseMember GetMemberByUserAndCourse(string personId, string courseId)
        {
            Person user = dbContext.Users.Include(u => u.CourseMemberships).ThenInclude(cm => cm.Course).SingleOrDefault(user => user.Id == personId);
            return user.CourseMemberships.SingleOrDefault(cm => cm.Course.Id.ToString() == courseId);
        }

        /// <inheritdoc/>
        public void RemoveMemberById(string id)
        {
            CourseMember cm = GetMemberByID(id);
            dbContext.CourseMembers.Remove(cm);
            dbContext.SaveChanges();
        }
    }
}