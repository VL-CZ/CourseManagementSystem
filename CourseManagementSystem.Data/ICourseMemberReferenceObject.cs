using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.Data
{
    /// <summary>
    /// interface for objects that have reference to <see cref="CourseMember"/>
    /// </summary>
    public interface ICourseMemberReferenceObject
    {
        /// <summary>
        /// course member that the object belongs to
        /// </summary>
        public CourseMember Student { get; set; }
    }
}