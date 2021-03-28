using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.ViewModels
{
    public class CourseTestVM
    {
        public CourseTestVM()
        {
        }

        public CourseTestVM(int id, string topic, int scoreWeight, IEnumerable<TestQuestionVM> questions)
        {
            Id = id;
            Topic = topic;
            Questions = questions;
            Weight = scoreWeight;
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
        /// questions in this test
        /// </summary>
        public IEnumerable<TestQuestionVM> Questions { get; set; }
    }
}
