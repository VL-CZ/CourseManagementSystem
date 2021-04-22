using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface IGradeService : ICourseReferenceService, IDbService
    {
        /// <summary>
        /// delete a grade with the given id
        /// </summary>
        /// <param name="gradeId">id of the grade to delete</param>
        void DeleteById(string gradeId);

        /// <summary>
        /// assign a grade to the student within <paramref name="grade"/> instance
        /// </summary>
        /// <param name="grade">grade to add to </param>
        void AssignGrade(Grade grade);
    }
}