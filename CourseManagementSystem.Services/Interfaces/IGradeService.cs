namespace CourseManagementSystem.Services.Interfaces
{
    public interface IGradeService : ICourseReferenceService
    {
        /// <summary>
        /// delete a grade with the given id
        /// </summary>
        /// <param name="gradeId">id of the grade to delete</param>
        void DeleteById(string gradeId);
    }
}