﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing one submission of the test
    /// </summary>
    public class TestSubmission : IGuidIdObject, ICourseMemberReferenceObject
    {
        public TestSubmission()
        {
            Answers = new List<TestSubmissionAnswer>();
        }

        /// <summary>
        /// create new test submission
        /// </summary>
        /// <param name="test">test that this submission belongs to</param>
        /// <param name="student">student that this submission belongs to</param>
        /// <param name="submittedAnswers">colleciton of submitted answers</param>
        public TestSubmission(CourseTest test, CourseMember student, ICollection<TestSubmissionAnswer> submittedAnswers)
        {
            Test = test;
            Student = student;
            Answers = submittedAnswers;
            IsReviewed = false;
            IsSubmitted = false;
        }

        /// <summary>
        /// identifier of the test submission
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// test that is submitted
        /// </summary>
        [Required]
        public CourseTest Test { get; set; }

        /// <summary>
        /// person who submitted this <see cref="TestSubmission"/>
        /// </summary>
        [Required]
        public CourseMember Student { get; set; }

        /// <summary>
        /// when was this test submitted
        /// </summary>
        public DateTime SubmittedDateTime { get; set; }

        /// <summary>
        /// has this test already been reviewed?
        /// </summary>
        public bool IsReviewed { get; set; }

        /// <summary>
        /// has this test been submitted?
        /// </summary>
        public bool IsSubmitted { get; set; }

        /// <summary>
        /// submitted answers
        /// </summary>
        public ICollection<TestSubmissionAnswer> Answers { get; set; }
    }
}