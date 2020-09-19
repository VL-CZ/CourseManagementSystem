﻿using CourseManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Data
{
    public class CMSDbContext : DbContext
    {
        private const string connectionString = "Server=.;Database=CMS;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
