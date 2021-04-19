using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing one post in a course forum
    /// </summary>
    public class ForumPost : IGuidIdObject, ICourseReferenceObject
    {
        public ForumPost() { }

        public ForumPost(string text, Person author, Course course)
        {
            Text = text;
            Author = author;
            Course = course;
        }

        /// <summary>
        /// identifier of the post 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// content of the post
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// author of the post
        /// </summary>
        public Person Author { get; set; }

        /// <summary>
        /// course that this posts belongs to
        /// </summary>
        public Course Course { get; set; }
    }
}
