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
            return dbContext.CourseMemberships.Include(x => x.User).Single(x => x.Id == id);
        }

        /// <inheritdoc/>
        public void RemoveMemberById(int id)
        {
            CourseMember cm = GetMemberByID(id);
            dbContext.CourseMemberships.Remove(cm);
            dbContext.SaveChanges();
        }
    }
}