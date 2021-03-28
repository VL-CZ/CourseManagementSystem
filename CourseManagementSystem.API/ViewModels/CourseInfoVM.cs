namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing info about a course
    /// </summary>
    public class CourseInfoVM
    {
        public CourseInfoVM(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// id of the course
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// name of the course
        /// </summary>
        public string Name { get; set; }
    }
}