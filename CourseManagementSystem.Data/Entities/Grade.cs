using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Entities
{
    public class Grade
    {
        [Key]
        public int ID { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; }
        public Person Student { get; set; }
    }
}
