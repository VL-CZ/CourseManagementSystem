using CourseManagementSystem.API.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing member of a course
    /// </summary>
    public class CourseMemberVM
    {
        public CourseMemberVM() { }

        public CourseMemberVM(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        /// <summary>
        /// identifier of the person
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// name of the person
        /// </summary>
        [MaxLength(ValidationConfig.maxStringLength)]
        public string Name { get; set; }

        /// <summary>
        /// email of the person
        /// </summary>
        [MaxLength(ValidationConfig.maxStringLength)]
        public string Email { get; set; }
    }
}