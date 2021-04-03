using CourseManagementSystem.Data.Models;
using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing a test in a course
    /// </summary>
    public class CourseTestVM
    {
        public CourseTestVM() { }

        public CourseTestVM(int id, string topic, int scoreWeight, IEnumerable<TestQuestionVM> questions, TestStatus testStatus)
        {
            Id = id;
            Topic = topic;
            Questions = questions;
            Weight = scoreWeight;
            Status = testStatus;
        }

        /// <summary>
        /// id of the test
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// weight of the score from the test (e.g. test of weight 2 has twice bigger impact on overall score than test of weight 1)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// status of the test see
        /// </summary>
        public TestStatus Status { get; set; }

        /// <summary>
        /// questions in this test
        /// </summary>
        public IEnumerable<TestQuestionVM> Questions { get; set; }
    }
}