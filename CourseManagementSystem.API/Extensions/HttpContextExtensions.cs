using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CourseManagementSystem.API.Extensions
{
    /// <summary>
    /// class containg extension methods for <see cref="HttpContext"/>
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// get ID of the current user
        /// </summary>
        /// <param name="httpContext">given HttpContext</param>
        /// <returns>ID of the current user</returns>
        public static string GetCurrentUserId(this HttpContext httpContext)
        {
            return httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}