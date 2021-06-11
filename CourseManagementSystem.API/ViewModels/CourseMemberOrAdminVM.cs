using CourseManagementSystem.API.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing member or admin of a course
    /// </summary>
    public class CourseMemberOrAdminVM
    {
        public CourseMemberOrAdminVM()
        {
        }

        public CourseMemberOrAdminVM(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        /// <summary>
        /// identifier of the person
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Id { get; set; }

        /// <summary>
        /// name of the person
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Name { get; set; }

        /// <summary>
        /// email of the person
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}