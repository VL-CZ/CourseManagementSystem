using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class Grade
    {
        public Grade() { }

        public Grade(int value, string comment, string topic)
        {
            Value = value;
            Comment = comment;
            Topic = topic;
        }

        [Key]
        public int ID { get; set; }

        /// <summary>
        /// numeric value of the grade
        /// </summary>
        [Required]
        public int Value { get; set; }

        /// <summary>
        /// topic of the grade
        /// </summary>
        [Required]
        public string Topic { get; set; }

        /// <summary>
        /// additional comment to the grade
        /// </summary>
        public string Comment { get; set; }
    }
}
