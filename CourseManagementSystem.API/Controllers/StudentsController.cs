using System;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private CMSDbContext dbContext;
        private IPersonService personService;

        public StudentsController(CMSDbContext dbContext, IPersonService personService)
        {
            this.dbContext = dbContext;
            this.personService = personService;
        }

        // GET: api/<StudentsController>
        [HttpGet]
        public IEnumerable<PersonVM> GetAll()
        {
            return dbContext.Users.Select(p => new PersonVM() { Id = p.Id, Name = p.UserName, Email = p.Email});
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return personService.GetPersonByID(id);
        }

        // POST api/<StudentsController>/5
        [HttpPost("{id}/assignGrade")]
        public void AssignGrade(int id, [FromBody] Grade g)
        {
            Person p = personService.GetPersonByID(id);
            p.AssignGrade(g);
            dbContext.SaveChanges();
        }
    }
}
