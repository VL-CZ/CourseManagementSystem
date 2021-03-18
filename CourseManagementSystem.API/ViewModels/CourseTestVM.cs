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

        public CourseTestVM(int id, string topic, ICollection<TestQuestion> questions)
        {
            Id = id;
            Topic = topic;
            Questions = questions;
        }

        public int Id { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// questions in this test
        /// </summary>
        public ICollection<TestQuestion> Questions { get; set; }
    }
}
