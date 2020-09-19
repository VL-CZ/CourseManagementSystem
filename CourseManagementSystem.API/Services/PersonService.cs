using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Entities;
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
            return dbContext.People.Find(id);
        }

        public void RemovePersonById(int personId)
        {
            Person p = GetPersonByID(personId);
            dbContext.People.Remove(p);
            dbContext.SaveChanges();
        }
    }
}
