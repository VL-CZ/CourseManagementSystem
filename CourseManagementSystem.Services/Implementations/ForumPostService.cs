using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
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
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void DeleteById(int postId)
        {
            var post = dbContext.Posts.Find(postId);
            dbContext.Posts.Remove(post);
            dbContext.SaveChanges();
        }
    }
}