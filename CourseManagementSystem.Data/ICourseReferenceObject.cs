using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.Data
{
    /// <summary>
    /// interface for objects that have reference to <see cref="Course"/>
    /// </summary>
    public interface ICourseReferenceObject
    {
        /// <summary>
        /// course that the object belongs to
        /// </summary>
        public Course Course { get; set; }
    }
}