﻿using System;
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

        /// <summary>
        /// create new post in a forum
        /// </summary>
        /// <param name="text">text of the post</param>
        /// <param name="author">author of the post</param>
        /// <param name="course">course where the post belongs to</param>
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
        [Required]
        public Person Author { get; set; }

        /// <summary>
        /// course that this posts belongs to
        /// </summary>
        [Required]
        public Course Course { get; set; }
    }
}
