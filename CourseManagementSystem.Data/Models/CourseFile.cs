using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    public class CourseFile : IGuidIdObject, ICourseReferenceObject
    {
        public CourseFile()
        {}

        public CourseFile(byte[] data, string name, string contentType)
        {
            Data = data;
            Name = name;
            ContentType = contentType;
        }

        /// <summary>
        /// identifier of the file
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// content of the file
        /// </summary>
        [Required]
        public byte[] Data { get; set; }

        /// <summary>
        /// name of the file
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// content type of the data
        /// </summary>
        [Required]
        public string ContentType { get; set; }

        /// <summary>
        /// course that this file belongs to
        /// </summary>
        [Required]
        public Course Course { get; set; }
    }
}