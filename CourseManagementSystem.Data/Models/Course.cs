using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class Course
    {
        public Course()
        {
            Members = new List<CourseMember>();
            Files = new List<CourseFile>();
            Tests = new List<CourseTest>();
            ForumPosts = new List<ForumPost>();
        }

        public Course(string name, Person admin) : this()
        {
            Name = name;
            Admin = admin;
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

        /// <summary>
        /// tests in this course
        /// </summary>
        public ICollection<CourseTest> Tests { get; set; }

        /// <summary>
        /// posts in the forum of this course
        /// </summary>
        public ICollection<ForumPost> ForumPosts { get; set; }
    }
}
