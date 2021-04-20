using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            return ;
        }
    }
}