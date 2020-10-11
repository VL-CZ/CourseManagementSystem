using CourseManagementSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private CMSDbContext dbContext;

        public FileController(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // POST api/<FileController>/upload
        [HttpPost("upload")]
        public void Upload([FromBody] FormFile file)
        {
        }
    }
}
