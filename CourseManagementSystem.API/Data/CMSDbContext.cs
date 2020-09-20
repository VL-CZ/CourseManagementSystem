using CourseManagementSystem.API.Models;
using CourseManagementSystem.Data.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.Data
{
    public class CMSDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public CMSDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
    }
}
