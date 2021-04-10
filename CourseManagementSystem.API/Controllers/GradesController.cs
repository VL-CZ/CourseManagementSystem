using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public void Delete(string id)
        {
            gradeService.DeleteById(id);
        }
    }
}