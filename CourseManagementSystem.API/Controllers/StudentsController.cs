using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseManagementSystem.API.Services;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<Person> GetAll()
        {
            return dbContext.People;
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return personService.GetPersonByID(id);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public void Add([FromBody] Person p)
        {
            dbContext.People.Add(p);
            dbContext.SaveChanges();
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Person p)
        {
            
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var p = personService.GetPersonByID(id);
            dbContext.People.Remove(p);
            dbContext.SaveChanges();
        }
    }
}
