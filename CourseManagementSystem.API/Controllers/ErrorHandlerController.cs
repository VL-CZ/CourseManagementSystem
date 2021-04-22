using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorHandlerController : ControllerBase
    {
        private const string generalErrorText = "An error occured.";
        
        /// <summary>
        /// handle runtime error
        /// </summary>
        /// <returns></returns>
        [Route("errorHandler")]
        public IActionResult ErrorHandler()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // thrown exception

            return Problem(generalErrorText, statusCode: 400);
        }
    }
}