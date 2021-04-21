using CourseManagementSystem.API.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// base viewmodel for a course
    /// </summary>
    public abstract class BaseCourseVM
    {
        protected BaseCourseVM()
        {
        }

        protected BaseCourseVM(string name)
        {
            Name = name;
        }

        /// <summary>
        /// name of the course to add
        /// </summary>
        [Required(ErrorMessage = ValidationConfig.requiredFieldErrorMessage)]
        public string Name { get; set; }
    }

    /// <summary>
    /// viewmodel representing info about a course
    /// </summary>
    public class CourseInfoVM : BaseCourseVM
    {
        public CourseInfoVM() : base()
        { }

        public CourseInfoVM(string id, string name):base(name)
        {
            Id = id;
        }

        /// <summary>
        /// id of the course
        /// </summary>
        [Required(ErrorMessage = ValidationConfig.requiredFieldErrorMessage)]
        public string Id { get; set; }
    }

    /// <summary>
    /// viewmodel for adding a course
    /// </summary>
    public class AddCourseVM : BaseCourseVM
    {
        public AddCourseVM()
        {
        }

        public AddCourseVM(string name, string adminId):base(name)
        {
            AdminId = adminId;
        }

        /// <summary>
        /// id of the admin
        /// </summary>
        [Required(ErrorMessage = ValidationConfig.requiredFieldErrorMessage)]
        public string AdminId { get; set; }
    }
}