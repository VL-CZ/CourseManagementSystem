using CourseManagementSystem.API.Auth.Factories;
using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CourseManagementSystem.API.Auth.Attributes
{
    /// <summary>
    /// filter authorizing admin of a course
    /// </summary>
    public class CourseAdminOrOwnerAuthorizeFilter : IAuthorizationFilter
    {
        private readonly EntityType entityType;
        private readonly IPeopleService peopleService;
        private readonly ICourseMemberService courseMemberService;
        private readonly ICourseReferenceServiceFactory courseReferenceServiceFactory;
        private readonly ICourseMemberReferenceServiceFactory courseMemberReferenceServiceFactory;
        private readonly string entityIdFieldName;

        public CourseAdminOrOwnerAuthorizeFilter(EntityType entityType, string entityIdFieldName,
            [FromServices] IPeopleService peopleService, [FromServices] ICourseMemberService courseMemberService,
            [FromServices] ICourseReferenceServiceFactory courseReferenceServiceFactory,
            [FromServices] ICourseMemberReferenceServiceFactory courseMemberReferenceServiceFactory)
        {
            this.entityType = entityType;
            this.peopleService = peopleService;
            this.courseMemberService = courseMemberService;
            this.courseReferenceServiceFactory = courseReferenceServiceFactory;
            this.entityIdFieldName = entityIdFieldName;
            this.courseMemberReferenceServiceFactory = courseMemberReferenceServiceFactory;
        }

        /// <inheritdoc/>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string currentUserId = context.HttpContext.GetCurrentUserId();
                string objectId = context.HttpContext.Request.RouteValues[entityIdFieldName].ToString();

                var courseRefService = courseReferenceServiceFactory.GetByEntityType(entityType);
                string courseId = courseRefService.GetCourseIdOf(objectId);

                var courseMemberRefService = courseMemberReferenceServiceFactory.GetByEntityType(entityType);
                string courseMemberId = courseMemberRefService.GetCourseMemberIdOf(objectId);

                if (peopleService.IsAdminOfCourse(currentUserId, courseId) || courseMemberService.BelongsTo(courseMemberId, currentUserId))
                {
                    // authorization passed -> proceed to controller
                    return;
                }
            }
            context.Result = new UnauthorizedResult();
        }
    }

    /// <summary>
    /// attribute for authorizing admin of a course
    /// </summary>
    public class AuthorizeCourseAdminOrOwnerOfAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// allow only course admin of a selected entity
        /// </summary>
        /// <param name="entityType">type of the selected entity</param>
        /// <param name="entityIdFieldName">name of the field that contains id of the entity</param>
        public AuthorizeCourseAdminOrOwnerOfAttribute(EntityType entityType, string entityIdFieldName) : base(typeof(CourseAdminAuthorizeFilter))
        {
            Arguments = new object[] { entityType, entityIdFieldName };
        }
    }
}