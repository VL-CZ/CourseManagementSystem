using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Data
{
    class CMSDbContext : DbContext
    {
        private const string connectionString = "Server=.;Database=CMS;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
