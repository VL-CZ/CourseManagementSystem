using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    public class Grade : IGuidIdObject, ICourseMemberReferenceObject
    {
        public Grade()
        {
        }

        public Grade(double percentualValue, string comment, string topic, int weight)
        {
            PercentualValue = percentualValue;
            Comment = comment;
            Topic = topic;
            Weight = weight;
        }

        /// <summary>
        /// identifier of the grade
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// percentual value of the grade (0=0%, 1=100%, may be greater than 1 in case of bonus points)
        /// </summary>
        public double PercentualValue { get; set; }

        /// <summary>
        /// weight of the grade - impact on total grade (the higher weight, the more impact)
        /// <br/>
        /// e.g. grade with weight 2 has the same weight as two grades weighted 1
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// topic of the grade
        /// </summary>
        [Required]
        public string Topic { get; set; }

        /// <summary>
        /// additional comment to the grade
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// course member that this grade belongs to
        /// </summary>
        [Required]
        public CourseMember Student { get; set; }
    }
}