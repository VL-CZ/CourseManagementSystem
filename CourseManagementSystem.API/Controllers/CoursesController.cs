﻿using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CMSDbContext dbContext;

        public CoursesController(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// create new course
        /// </summary>
        /// <returns></returns>
        [HttpPost("create")]
        public CourseInfoVM Create([FromBody] AddCourseVM courseVM)
        {
            Person admin = dbContext.Users.Single(x => x.Id == courseVM.AdminId);
            Course createdCourse = new Course(courseVM.Name, admin);

            dbContext.Courses.Add(createdCourse);
            dbContext.SaveChanges();

            return new CourseInfoVM(createdCourse.Id, createdCourse.Name);
        }

        /// <summary>
        /// delete course by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Course c = dbContext.Courses.Find(id);
            dbContext.Courses.Remove(c);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// get all course members
        /// </summary>
        /// <param name="id">Id of the course</param>
        [HttpGet("{id}/members")]
        public IEnumerable<CourseMemberVM> GetAllMembers(int id)
        {
            var course = dbContext.Courses.Include(x => x.Members).Single(x => x.Id == id);
            var courseMemberIDs = course.Members.Select(x => x.Id);
            var people = dbContext.CourseMembers.Include(x => x.User).Where(cm => courseMemberIDs.Contains(cm.Id));

            return people.Select(cm => new CourseMemberVM(cm.Id.ToString(), cm.User.UserName, cm.User.Email));
        }

        /// <summary>
        /// get all shared files in the course with given id
        /// </summary>
        /// <param name="id">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/files")]
        public IEnumerable<CourseFileVM> GetAllFiles(int id)
        {
            return dbContext.Courses.Include(c => c.Files).Single(x => x.Id == id).Files.Select(f => new CourseFileVM { Id = f.ID, Name = f.Name });
        }

        /// <summary>
        /// get all tests in the course with given id
        /// </summary>
        /// <param name="id">Id of the course</param>
        /// <returns></returns>
        [HttpGet("{id}/tests")]
        public IEnumerable<CourseTestVM> GetAllTests(int id)
        {
            var courseTests = dbContext.Courses.Include(course => course.Tests).Single(x => x.Id == id).Tests;
            return courseTests.Select(ct => new CourseTestVM(ct.Id, ct.Topic, ct.Weight, ct.Questions.ToViewModels()));
        }
    }
}