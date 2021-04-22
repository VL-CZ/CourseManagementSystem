using CourseManagementSystem.Data;

namespace CourseManagementSystem.Services
{
    /// <summary>
    /// class representing base database service
    /// </summary>
    public abstract class DbService : IDbService
    {
        /// <summary>
        /// context of the CMS database
        /// </summary>
        protected readonly CMSDbContext dbContext;

        /// <summary>
        /// construct a new database service
        /// </summary>
        /// <param name="dbContext">CMS database context</param>
        protected DbService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void CommitChanges()
        {
            dbContext.SaveChanges();
        }
    }
}