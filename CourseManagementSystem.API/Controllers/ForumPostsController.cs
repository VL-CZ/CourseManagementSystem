﻿using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumPostsController : ControllerBase
    {
        private readonly IForumPostService forumPostService;
        private readonly ICourseService courseService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPeopleService peopleService;

        public ForumPostsController(IForumPostService forumPostService, IHttpContextAccessor httpContextAccessor, ICourseService courseService, IPeopleService peopleService)
        {
            this.forumPostService = forumPostService;
            this.httpContextAccessor = httpContextAccessor;
            this.courseService = courseService;
            this.peopleService = peopleService;
        }

        /// <summary>
        /// add a post to the course
        /// </summary>
        /// <param name="courseId">id of the course</param>
        /// <param name="forumPostVM">post to add</param>
        [HttpPost("{courseId}")]
        public void AddToCourse(int courseId, ForumPostVM forumPostVM)
        {
            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();

            Course course = courseService.GetById(courseId);
            Person person = peopleService.GetById(currentUserId);

            forumPostService.AddPostTo(forumPostVM.Text, course, person);
        }

        /// <summary>
        /// delete a post by its id
        /// </summary>
        /// <param name="postId">id of the post to delete</param>
        [HttpDelete("{postId}")]
        public void Delete(int postId)
        {
            forumPostService.DeleteById(postId);
        }
    }
}