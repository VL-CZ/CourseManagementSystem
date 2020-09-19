using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}
