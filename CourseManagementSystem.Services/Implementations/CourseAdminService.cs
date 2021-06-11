using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseManagementSystem.Services.Implementations
{
    public class CourseAdminService : DbService, ICourseAdminService
    {
        public CourseAdminService(CMSDbContext dbContext) : base(dbContext)
        { }

        /// <inheritdoc/>
        public string GetCourseIdOf(string objectId)
        {
            return dbContext.CourseAdmins.GetCourseIdOf(objectId);
        }

        /// <inheritdoc/>
        public void RemoveById(string courseAdminId)
        {
            var adminToRemove = dbContext.CourseAdmins.FindById(courseAdminId);
            dbContext.CourseAdmins.Remove(adminToRemove);
        }
    }
}
