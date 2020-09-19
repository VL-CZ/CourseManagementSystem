using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Grade
    {
        public int ID { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; }
        public Person Student { get; set; }
    }
}
