using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// get current user id
        /// </summary>
        /// <returns></returns>
        [HttpGet("getId")]
        public PersonIdVM GetId()
        {
            string userId = httpContextAccessor.HttpContext.GetCurrentUserId();
            return new PersonIdVM() { Id = userId };
        }

        /// <summary>
        /// determine if current user is admin
        /// </summary>
        /// <returns></returns>
        [HttpGet("isAdmin")]
        public IsAdminVM IsAdmin()
        {
            // TO-DO: add roles
            return new IsAdminVM() { IsAdmin = httpContextAccessor.HttpContext.GetCurrentUserId() == "b7a6f405-c226-4f5a-a0cb-2ba4c47582a3" };
        }
    }
}
