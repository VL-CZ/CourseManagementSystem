using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService gradeService;

        public GradesController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        /// <summary>
        /// remove grade with selected id
        /// </summary>
        /// <param name="id">id of the grade</param>
        [HttpDelete("delete/{id}")]
        public void Delete(int id)
        {
            gradeService.DeleteById(id);
        }
    }
}