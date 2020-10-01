using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Data.Models
{
    public class Person : IdentityUser
    {
        public ICollection<Grade> Grades { get; set; }

        public void AssignGrade(Grade g)
        {
            Grades.Add(g);
        }
    }
}
