using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using CourseManagementSystem.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

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
                .Single(user => user.Id == personId);
            return user.CourseMemberships.Single(cm => cm.Course.Id.ToString() == courseId && !cm.IsArchived);
        }

        /// <inheritdoc/>
        public void ArchiveMemberById(string id)
        {
            CourseMember cm = GetMemberWithUser(id);
            cm.IsArchived = true;
        }

        /// <inheritdoc/>
        public IEnumerable<Grade> GetGradesOf(string courseMemberId)
        {
            return dbContext.CourseMembers
                .Include(cm => cm.Grades)
                .Single(cm => cm.Id.ToString() == courseMemberId)
                .Grades;
        }
    }
}