using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing user of the application
    /// </summary>
    public class Person : IdentityUser
    {
        /// <summary>
        /// grades of this student
        /// </summary>
        public ICollection<Grade> Grades { get; set; }

        /// <summary>
        /// courses, where the user is enrolled
        /// </summary>
        //public ICollection<Course> Courses { get; set; }

        /// <summary>
        /// add new grade to this person
        /// </summary>
        /// <param name="g"></param>
        public void AssignGrade(Grade g)
        {
            Grades.Add(g);
        }
    }
}
