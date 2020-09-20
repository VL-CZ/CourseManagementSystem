using CourseManagementSystem.API.Data;
using CourseManagementSystem.API;
using CourseManagementSystem.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.Services
{
    class PersonService : IPersonService
    {
        private CMSDbContext dbContext;

        public PersonService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Person GetPersonByID(int id)
        {
            return dbContext.Users.Find(id);
        }

        public void RemovePersonById(int personId)
        {
            Person p = GetPersonByID(personId);
            dbContext.Users.Remove(p);
            dbContext.SaveChanges();
        }
    }
}
