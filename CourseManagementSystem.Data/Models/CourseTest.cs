using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CourseManagementSystem.Data.Models
{
    public class CourseTest : IGuidIdObject, ICourseReferenceObject
    {
        public CourseTest()
        {
            Questions = new List<TestQuestion>();
            Submissions = new List<TestSubmission>();
            Status = TestStatus.New;
        }

        public CourseTest(string topic, ICollection<TestQuestion> questions, int weight, DateTime deadline) : this()
        {
            Topic = topic;
            Questions = questions;
            Weight = weight;
            Deadline = deadline;
        }

        /// <summary>
        /// identifier of the test
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        [Required]
        public string Topic { get; set; }

        /// <summary>
        /// weight of the score from the test (e.g. test of weight 2 has twice bigger impact on overall score than test of weight 1)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// status of the test
        /// </summary>
        public TestStatus Status { get; set; }

        /// <summary>
        /// deadline of the test (day and exact time)
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// course that contains this test
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// questions in this test
        /// </summary>
        public ICollection<TestQuestion> Questions { get; set; }

        /// <summary>
        /// students' submissions of this test
        /// </summary>
        public ICollection<TestSubmission> Submissions { get; set; }
    }

    /// <summary>
    /// enum representing status of the <see cref="CourseTest"/>
    /// </summary>
    public enum TestStatus
    {
        /// <summary>
        /// test hasn't been published yet
        /// </summary>
        New,

        /// <summary>
        /// test has been published
        /// </summary>
        Published
    }
}