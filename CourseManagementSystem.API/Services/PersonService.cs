using CourseManagementSystem.API.Data;
using CourseManagementSystem.API;
using CourseManagementSystem.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CourseManagementSystem.API.Services
{
    class PersonService : IPersonService
    {
        private CMSDbContext dbContext;

        public PersonService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Person GetPersonByID(string id)
        {
            return dbContext.Users.Include(x => x.Grades).Single(x => x.Id == id);
        }

        public void RemovePersonById(string id)
        {
            Person p = GetPersonByID(id);
            dbContext.Users.Remove(p);
            dbContext.SaveChanges();
        }
    }
}
