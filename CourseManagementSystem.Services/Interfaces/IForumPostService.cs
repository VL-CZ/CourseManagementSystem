using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface IForumPostService
    {
        /// <summary>
        /// delete post by its id
        /// </summary>
        /// <param name="postId">identifier of the post</param>
        void DeleteById(string postId);

        /// <summary>
        /// add post to the course
        /// </summary>
        /// <param name="postContent">content of the post</param>
        /// <param name="course">course where this post belongs to</param>
        /// <param name="person">author of the post</param>
        void AddPostTo(string postContent, Course course, Person author);
    }
}