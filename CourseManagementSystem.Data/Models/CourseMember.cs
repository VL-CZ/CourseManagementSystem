using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class CourseMember
    {
        public CourseMember()
        {
            Grades = new List<Grade>();
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// reference to person
        /// </summary>
        public Person User { get; set; }

        /// <summary>
        /// reference to course
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// list of grades
        /// </summary>
        public ICollection<Grade> Grades { get; set; }

        /// <summary>
        /// add new grade to this course membership
        /// </summary>
        /// <param name="g"></param>
        public void AssignGrade(Grade g)
        {
            Grades.Add(g);
        }
    }
}
