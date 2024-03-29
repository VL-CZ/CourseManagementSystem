﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing request for enrollment to a course
    /// </summary>
    public class EnrollmentRequest : IGuidIdObject, ICourseReferenceObject
    {
        public EnrollmentRequest()
        {
        }

        /// <summary>
        /// create new class representing request for enrollment to a course
        /// </summary>
        /// <param name="course">course where the request is sent to</param>
        /// <param name="person">person that sent the request</param>
        public EnrollmentRequest(Course course, Person person)
        {
            Course = course;
            Person = person;
        }

        /// <summary>
        /// identifier of the enrollment request
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// course that we want to enroll to
        /// </summary>
        [Required]
        public Course Course { get; set; }

        /// <summary>
        /// person that created the enrollment request
        /// </summary>
        [Required]
        public Person Person { get; set; }
    }
}