namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel for adding a course
    /// </summary>
    public class AddCourseVM
    {
        /// <summary>
        /// name of the course to add
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// id of the admin
        /// </summary>
        public string AdminId { get; set; }
    }
}