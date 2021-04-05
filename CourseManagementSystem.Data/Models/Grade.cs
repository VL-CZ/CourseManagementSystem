﻿using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Data.Models
{
    public class Grade
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
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// percentual value of the grade (0=0%, 1=100%, may be greater than 1 in case of bonus points)
        /// </summary>
        [Required]
        public double PercentualValue { get; set; }

        /// <summary>
        /// weight of the grade - impact on total grade (the higher weight, the more impact)
        /// <br/>
        /// e.g. grade with weight 2 has the same weight as two grades weighted 1
        /// </summary>
        [Required]
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
    }
}