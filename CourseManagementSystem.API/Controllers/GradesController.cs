using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private CMSDbContext dbContext;

        public GradesController(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<GradesController>
        [HttpGet]
        public IEnumerable<Grade> Get()
        {
            return dbContext.Grades;
        }

        // GET api/<GradesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GradesController>
        [HttpPost]
        public void Post([FromBody] Grade g)
        {
            dbContext.Grades.Add(g);
            dbContext.SaveChanges();
        }

        // PUT api/<GradesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GradesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
