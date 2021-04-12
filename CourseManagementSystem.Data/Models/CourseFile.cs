using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    public class CourseFile : IGuidIdObject, ICourseReferenceObject
    {
        /// <summary>
        /// identifier of the file
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// content of the file
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// name of the file
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// content type of the data
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// course that this file belongs to
        /// </summary>
        public Course Course { get; set; }
    }
}