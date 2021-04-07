﻿namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// class representing viewmodel for a forum post
    /// </summary>
    public class ForumPostVM
    {
        public ForumPostVM()
        { }

        public ForumPostVM(string id, string author, string text)
        {
            Id = id;
            Author = author;
            Text = text;
        }

        /// <summary>
        /// identifier of the post
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// author of the post
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// content of the post
        /// </summary>
        public string Text { get; set; }
    }
}