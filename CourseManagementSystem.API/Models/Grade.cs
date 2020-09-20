using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.API.Models
{
    public class Grade
    {
        public Grade() { }

        public Grade(int value, string comment)
        {
            Value = value;
            Comment = comment;
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public int Value { get; set; }
        
        public string Comment { get; set; }
    }
}
