using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    public class CourseMember : IGuidIdObject, ICourseReferenceObject
    {
        public CourseMember()
        {
            Grades = new List<Grade>();
            TestSubmissions = new List<TestSubmission>();
        }

        public CourseMember(Person user, Course course) : this()
        {
            User = user;
            Course = course;
            IsArchived = false;
        }

        /// <summary>
        /// identifier of the course member
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// reference to person
        /// </summary>
        [Required]
        public Person User { get; set; }

        /// <summary>
        /// reference to course
        /// </summary>
        [Required]
        public Course Course { get; set; }

        /// <summary>
        /// check if this course member has been archived
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// list of grades
        /// </summary>
        public ICollection<Grade> Grades { get; set; }

        /// <summary>
        /// tests submitted by this user
        /// </summary>
        public ICollection<TestSubmission> TestSubmissions { get; set; }
    }
}