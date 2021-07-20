using CourseManagementSystem.API.Validation.Attributes;

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

        /// <summary>
        /// create new instance of base viewmodel for courses
        /// </summary>
        /// <param name="name">name of the course</param>
        protected BaseCourseVM(string name)
        {
            Name = name;
        }

        /// <summary>
        /// name of the course to add
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Name { get; set; }
    }

    /// <summary>
    /// viewmodel representing info about a course
    /// </summary>
    public class CourseInfoVM : BaseCourseVM
    {
        public CourseInfoVM() : base()
        { }

        /// <summary>
        /// create new viewmodel representing info about a course
        /// </summary>
        /// <param name="id">id of the course</param>
        /// <param name="name">name of the course</param>
        public CourseInfoVM(string id, string name) : base(name)
        {
            Id = id;
        }

        /// <summary>
        /// id of the course
        /// </summary>
        [RequiredWithDefaultErrorMessage]
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

        public AddCourseVM(string name) : base(name)
        {
        }
    }
}