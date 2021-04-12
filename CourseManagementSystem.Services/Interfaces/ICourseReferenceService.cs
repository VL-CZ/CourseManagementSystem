namespace CourseManagementSystem.Services.Interfaces
{
    /// <summary>
    /// interface for services of entities that logically belong to a course
    /// </summary>
    public interface ICourseReferenceService
    {
        /// <summary>
        /// get id of course that the object belongs to
        /// </summary>
        /// <param name="objectId">id of the object</param>
        /// <returns></returns>
        string GetCourseIdOf(string objectId);
    }
}