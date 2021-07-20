using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing admin of a course relation
    /// </summary>
    public class CourseAdmin : IGuidIdObject, ICourseReferenceObject
    {
        public CourseAdmin()
        { }

        /// <summary>
        /// create new admin membership entity
        /// </summary>
        /// <param name="user">admin of the <paramref name="course"/></param>
        /// <param name="course">course that <paramref name="user"/> will manages</param>
        public CourseAdmin(Person user, Course course) : this()
        {
            User = user;
            Course = course;
        }

        /// <summary>
        /// identifier of the CourseAdmin
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// admin of the <see cref="Course"/>
        /// </summary>
        [Required]
        public Person User { get; set; }

        /// <summary>
        /// course that this entity manages
        /// </summary>
        [Required]
        public Course Course { get; set; }
    }
}