using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.Data;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.Auth
{
    public class CourseAdminRequirement : IAuthorizationRequirement
    {
        public const string testIdFieldName = "testId";
        public const string policyName = "IsCourseAdmin";
    }

    public class CourseAdminHandler : AuthorizationHandler<CourseAdminRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPeopleService peopleService;
        private readonly ICourseTestService courseTestService;

        public CourseAdminHandler(IHttpContextAccessor httpContextAccessor, IPeopleService peopleService, ICourseTestService courseTestService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.peopleService = peopleService;
            this.courseTestService = courseTestService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CourseAdminRequirement requirement)
        {
            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
            string testId = httpContextAccessor.HttpContext.Request.RouteValues[CourseAdminRequirement.testIdFieldName].ToString();

            string courseId = courseTestService.GetCourseId(testId);

            if (peopleService.IsAdminOfCourse(currentUserId, courseId))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }

}