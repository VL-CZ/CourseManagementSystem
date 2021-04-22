using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using CourseManagementSystem.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class CourseMemberService : DbService, ICourseMemberService
    {
        public CourseMemberService(CMSDbContext dbContext) : base(dbContext)
        { }

        /// <inheritdoc/>
        public bool BelongsTo(string courseMemberId, string personId)
        {
            return GetMemberWithUser(courseMemberId).User.Id == personId;
        }

        ///<inheritdoc/>
        public string GetCourseIdOf(string objectId)
        {
            return dbContext.CourseMembers.GetCourseIdOf(objectId);
        }

        /// <inheritdoc/>
        public CourseMember GetMemberWithUser(string id)
        {
            return dbContext.CourseMembers
                .Include(cm => cm.User)
                .Single(cm => cm.Id.ToString() == id);
        }

        /// <inheritdoc/>
        public CourseMember GetMemberByUserAndCourse(string personId, string courseId)
        {
            Person user = dbContext.Users
                .Include(user => user.CourseMemberships)
                .ThenInclude(cm => cm.Course)
                .SingleOrDefault(user => user.Id == personId);
            return user.CourseMemberships.SingleOrDefault(cm => cm.Course.Id.ToString() == courseId);
        }

        /// <inheritdoc/>
        public void RemoveMemberById(string id)
        {
            CourseMember cm = GetMemberWithUser(id);
            dbContext.CourseMembers.Remove(cm);
        }
    }
}