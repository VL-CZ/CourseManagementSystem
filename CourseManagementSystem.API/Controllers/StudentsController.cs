﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseManagementSystem.API.Data;
using CourseManagementSystem.API.Services;
using CourseManagementSystem.API;
using CourseManagementSystem.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Extensions;
using CourseManagementSystem.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private CMSDbContext dbContext;
        private IPersonService personService;
        private readonly UserManager<Person> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StudentsController(CMSDbContext dbContext, IPersonService personService, UserManager<Person> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.personService = personService;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<StudentsController>
        [HttpGet]
        public IEnumerable<PersonVM> GetAll()
        {
            return dbContext.Users.Select(p => new PersonVM() { Id = p.Id, Name = p.UserName, Email = p.Email });
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public StudentVM Get(string id)
        {
            Person p = personService.GetPersonByID(id);
            return new StudentVM
            {
                Email = p.Email,
                Id = p.Id,
                Name = p.UserName,
                Grades = p.Grades.Select(g => new GradeDetailsVM() { Id = g.ID, Comment = g.Comment, Topic = g.Topic, Value = g.Value })
            };
        }

        // POST api/<StudentsController>/5
        [HttpPost("{id}/assignGrade")]
        public GradeDetailsVM AssignGrade(string id, [FromBody] AddGradeVM g)
        {
            Person p = personService.GetPersonByID(id);
            Grade grade = new Grade { Value = g.Value, Comment = g.Comment, Topic = g.Topic };
            p.AssignGrade(grade);
            dbContext.SaveChanges();

            return new GradeDetailsVM() { Id = grade.ID, Comment = grade.Comment, Value = grade.Value, Topic = grade.Topic };
        }

        [Authorize]
        [HttpGet("getId")]
        public PersonIdVM GetId()
        {
            string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return new PersonIdVM() { Id = userId };
        }
    }
}
