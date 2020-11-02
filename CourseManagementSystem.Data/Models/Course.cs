using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class Course
    {
        public Course() { }

        public Course(string name, Person admin)
        {
            Name = name;
            Admin = admin;
            Members = new List<CourseMember>();
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// name of the course
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// admin of the course
        /// </summary>
        [Required]
        public Person Admin { get; set; }

        /// <summary>
        /// members of the course (except admin)
        /// </summary>
        public ICollection<CourseMember> Members { get; set; }

        /// <summary>
        /// shared files in this course
        /// </summary>
        public ICollection<CourseFile> Files { get; set; }
    }
}
