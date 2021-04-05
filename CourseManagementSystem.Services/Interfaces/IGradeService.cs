namespace CourseManagementSystem.Services.Interfaces
{
    public interface IGradeService
    {
        /// <summary>
        /// delete a grade with the given id
        /// </summary>
        /// <param name="gradeId">id of the grade to delete</param>
        void DeleteById(int gradeId);
    }
}