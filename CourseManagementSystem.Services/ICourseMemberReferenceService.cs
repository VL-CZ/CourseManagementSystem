namespace CourseManagementSystem.Services
{
    /// <summary>
    /// interface for services of entities that logically belong to a course member
    /// </summary>
    public interface ICourseMemberReferenceService
    {
        /// <summary>
        /// get id of <see cref="Data.Models.CourseMember"/> that the object belongs to
        /// </summary>
        /// <param name="objectId">id of the object</param>
        /// <returns></returns>
        string GetCourseMemberIdOf(string objectId);
    }
}