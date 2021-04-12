using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var collections = new ICourseReferenceService[] { courseTestService };
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CourseAdminRequirement requirement)
        {
            string currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
            string testId = httpContextAccessor.HttpContext.Request.RouteValues[CourseAdminRequirement.testIdFieldName].ToString();

            string courseId = courseTestService.GetCourseIdOf(testId);

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