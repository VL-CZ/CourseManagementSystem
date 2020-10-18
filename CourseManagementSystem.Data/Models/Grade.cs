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

        [Required]
        public int Value { get; set; }

        [Required]
        public string Topic { get; set; }

        public string Comment { get; set; }
    }
}
