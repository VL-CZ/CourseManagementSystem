using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    public class CourseMember : IGuidIdObject
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
        public Person User { get; set; }

        /// <summary>
        /// reference to course
        /// </summary>
        public Course Course { get; set; }

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