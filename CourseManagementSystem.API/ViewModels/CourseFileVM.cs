using CourseManagementSystem.API.Validation.Attributes;

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

        /// <summary>
        /// create new instance of this viewmodel
        /// </summary>
        /// <param name="id">identifier of the file</param>
        /// <param name="name">name of the file</param>
        public CourseFileVM(string id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// identifier of the file
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Id { get; set; }

        /// <summary>
        /// name of the file
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Name { get; set; }
    }
}