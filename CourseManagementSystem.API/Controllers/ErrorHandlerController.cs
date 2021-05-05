using CourseManagementSystem.API.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CourseManagementSystem.API.Controllers
{
    /// <summary>
    /// controller for error handling
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorHandlerController : ControllerBase
    {
        private const string generalErrorText = "An error occurred while processing the request.";

        /// <summary>
        /// handle runtime error
        /// </summary>
        /// <returns></returns>
        [Route("errorHandler")]
        public IActionResult ErrorHandler()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // thrown exception

            var errorDescription = new Dictionary<string, string[]>
            {
                { "Request failed", new string[] { generalErrorText } }
            };

            var errorsVM = new ErrorsDictionaryVM(errorDescription);
            return BadRequest(errorsVM);
        }
    }
}