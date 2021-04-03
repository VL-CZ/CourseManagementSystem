using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;

namespace CourseManagementSystem.Services.Implementations
{
    public class PeopleService : IPeopleService
    {
        private readonly CMSDbContext dbContext;

        public PeopleService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public Person GetById(string personId)
        {
            return dbContext.Users.Find(personId);
        }
    }
}