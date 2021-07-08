using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    public class Course : IGuidIdObject
    {
        public Course()
        {
            Members = new List<CourseMember>();
            Files = new List<CourseFile>();
            Tests = new List<CourseTest>();
            ForumPosts = new List<ForumPost>();
            Admins = new List<CourseAdmin>();
            EnrollmentRequests = new List<EnrollmentRequest>();
        }

        public Course(string name, Person admin) : this()
        {
            Name = name;
            Admins = new List<CourseAdmin>() { new CourseAdmin(admin, this) };
            IsArchived = false;
        }

        /// <summary>
        /// identifier of the couse
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// check if this course has been archived
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// name of the course
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// admin of the course
        /// </summary>
        public ICollection<CourseAdmin> Admins { get; set; }

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

        /// <summary>
        /// enrollment requests to this course
        /// </summary>
        public ICollection<EnrollmentRequest> EnrollmentRequests { get; set; }
    }
}