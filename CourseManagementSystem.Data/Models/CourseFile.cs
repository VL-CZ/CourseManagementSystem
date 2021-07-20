using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// entity representing shared file in a course
    /// </summary>
    public class CourseFile : IGuidIdObject, ICourseReferenceObject
    {
        public CourseFile()
        { }

        /// <summary>
        /// create new entity representing shared file
        /// </summary>
        /// <param name="data">file data</param>
        /// <param name="name">name of the file</param>
        /// <param name="contentType">content type of the file</param>
        /// <param name="course">course where the file is shared</param>
        public CourseFile(byte[] data, string name, string contentType, Course course)
        {
            Data = data;
            Name = name;
            ContentType = contentType;
            Course = course;
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