﻿using CourseManagementSystem.API.Validation.Attributes;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// class representing viewmodel for a forum post
    /// </summary>
    public class ForumPostVM
    {
        public ForumPostVM()
        { }

        /// <summary>
        /// create new viewmodel for forum post
        /// </summary>
        /// <param name="id">id of the post</param>
        /// <param name="author">name of the author of the post</param>
        /// <param name="text">text of the post</param>
        public ForumPostVM(string id, string author, string text)
        {
            Id = id;
            Author = author;
            Text = text;
        }

        /// <summary>
        /// identifier of the post
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Id { get; set; }

        /// <summary>
        /// author of the post
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Author { get; set; }

        /// <summary>
        /// content of the post
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Text { get; set; }
    }
}