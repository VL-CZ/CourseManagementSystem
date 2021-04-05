namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing a shared file in the course
    /// </summary>
    public class CourseFileVM
    {
        public CourseFileVM()
        {
        }

        public CourseFileVM(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// identifier of the file
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// name of the file
        /// </summary>
        public string Name { get; set; }
    }
}