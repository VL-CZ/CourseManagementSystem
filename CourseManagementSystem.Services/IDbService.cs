namespace CourseManagementSystem.Services
{
    public interface IDbService
    {
        /// <summary>
        /// commit changes to the database
        /// </summary>
        void CommitChanges();
    }
}