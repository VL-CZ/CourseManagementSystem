using CourseManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly CMSDbContext dbContext;

        public GradesController(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// remove grade with selected id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("delete/{id}")]
        public void Delete(int id)
        {
            var grade = dbContext.Grades.Find(id);
            dbContext.Grades.Remove(grade);
            dbContext.SaveChanges();
        }
    }
}