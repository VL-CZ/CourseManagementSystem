using CourseManagementSystem.API.Validation.Attributes;
using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// base viewmodel for course tests
    /// </summary>
    public abstract class BaseCourseTestVM
    {
        protected BaseCourseTestVM()
        {
        }

        /// <summary>
        /// create new instance of base viewmodel for course tests
        /// </summary>
        /// <param name="weight">weight of the test</param>
        /// <param name="topic">topic of the test</param>
        /// <param name="deadline">deadline of the test</param>
        /// <param name="questions">questions contained in the test</param>
        /// <param name="isGraded">is this test graded?</param>
        protected BaseCourseTestVM(int weight, string topic, DateTime deadline, IEnumerable<TestQuestionVM> questions, bool isGraded)
        {
            Weight = weight;
            Topic = topic;
            Deadline = deadline;
            Questions = questions;
            IsGraded = isGraded;
        }

        /// <summary>
        /// weight of the score from the test (e.g. test of weight 2 has twice bigger impact on overall score than test of weight 1)
        /// </summary>
        [PositiveIntValue]
        public int Weight { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        //[MaxLength(ValidationConfig.maxStringLength)]
        [RequiredWithDefaultErrorMessage]
        public string Topic { get; set; }

        /// <summary>
        /// deadline of the test
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// is this test graded?
        /// <br/>
        /// if not -> it's a quiz
        /// </summary>
        public bool IsGraded { get; set; }

        /// <summary>
        /// questions in this test
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public IEnumerable<TestQuestionVM> Questions { get; set; }
    }

    /// <summary>
    /// viewmodel for adding a course test
    /// </summary>
    public class AddCourseTestVM : BaseCourseTestVM
    {
        public AddCourseTestVM() : base()
        { }
    }

    /// <summary>
    /// viewmodel representing a test in a course
    /// </summary>
    public class CourseTestDetailsVM : BaseCourseTestVM
    {
        public CourseTestDetailsVM() : base()
        { }

        /// <summary>
        /// create new instance of base viewmodel for course tests details
        /// </summary>
        /// <param name="topic">topic of the test</param>
        /// <param name="deadline">deadline of the test</param>
        /// <param name="questions">questions contained in the test</param>
        /// <param name="isGraded">is this test graded?</param>
        /// <param name="id">id of the course test</param>
        /// <param name="scoreWeight">weight of the test</param>
        /// <param name="testStatus"></param>
        public CourseTestDetailsVM(string id, string topic, int scoreWeight, IEnumerable<TestQuestionVM> questions, TestStatus testStatus, DateTime deadline, bool isGraded)
            : base(scoreWeight, topic, deadline, questions, isGraded)
        {
            Id = id;
            Status = testStatus;
        }

        /// <summary>
        /// id of the test
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Id { get; set; }

        /// <summary>
        /// status of the test
        /// </summary>
        public TestStatus Status { get; set; }
    }
}