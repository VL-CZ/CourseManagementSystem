using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Services
{
    public class PersonService : IPersonService
    {
        private CMSDbContext dbContext;

        public PersonService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public Person GetPersonByID(string id)
        {
            return dbContext.Users.Include(x => x.Grades).Single(x => x.Id == id);
        }

        /// <inheritdoc/>
        public void RemovePersonById(string id)
        {
            Person p = GetPersonByID(id);
            dbContext.Users.Remove(p);
            dbContext.SaveChanges();
        }
    }
}