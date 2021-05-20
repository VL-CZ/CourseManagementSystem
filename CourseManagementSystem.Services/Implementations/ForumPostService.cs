using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
using CourseManagementSystem.Services.Interfaces;

namespace CourseManagementSystem.Services.Implementations
{
    public class ForumPostService : DbService, IForumPostService
    {
        public ForumPostService(CMSDbContext dbContext) : base(dbContext)
        {
        }

        /// <inheritdoc/>
        public void AddPostTo(string postContent, Course course, Person author)
        {
            var post = new ForumPost(postContent, author, course);
            dbContext.Posts.Add(post);
        }

        /// <inheritdoc/>
        public void DeleteById(string postId)
        {
            var post = GetById(postId);
            dbContext.Posts.Remove(post);
        }

        ///<inheritdoc/>
        public string GetCourseIdOf(string objectId)
        {
            return dbContext.Posts.GetCourseIdOf(objectId);
        }

        /// <summary>
        /// find a post by its id
        /// </summary>
        /// <param name="postId">identifier of the post</param>
        /// <returns></returns>
        private ForumPost GetById(string postId)
        {
            return dbContext.Posts.FindById(postId);
        }
    }
}