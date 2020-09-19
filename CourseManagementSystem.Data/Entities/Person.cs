using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Entities
{
    public class Person
    {
        public Person(string name, string email)
        {
            Name = name;
            Email = email;
            Grades = new List<Grade>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}
