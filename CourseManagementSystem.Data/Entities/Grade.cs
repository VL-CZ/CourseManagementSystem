using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Entities
{
    public class Grade
    {
        public Grade(int value, string comment, Person student)
        {
            Value = value;
            Comment = comment;
            Student = student;
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public int Value { get; set; }
        
        public string Comment { get; set; }

        [Required]
        public Person Student { get; set; }
    }
}
