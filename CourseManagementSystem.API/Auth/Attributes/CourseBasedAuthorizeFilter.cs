using CourseManagementSystem.API.Auth.Factories;
using CourseManagementSystem.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CourseManagementSystem.API.Auth.Attributes
{
    /// <summary>
    /// filter for authorization based on <see cref="Data.Models.Course"/> related entity
    /// </summary>
    public abstract class CourseBasedAuthorizeFilter : IAuthorizationFilter
    {
        /// <summary>
        /// type of the course related entity
        /// </summary>
        private readonly EntityType entityType;

        /// <summary>
        /// name of the field in HTTP route that contains the id of the entity
        /// </summary>
        private readonly string entityIdFieldName;

        /// <summary>
        /// factory for <see cref="Services.Interfaces.ICourseReferenceService"/> services
        /// </summary>
        private readonly ICourseReferenceServiceFactory courseReferenceServiceFactory;

        /// <summary>
        /// create new instance of filter based on course authorization
        /// </summary>
        /// <param name="entityType">type of the entity</param>
        /// <param name="entityIdFieldName">name of the field with entity id within HTTP route</param>
        /// <param name="courseReferenceServiceFactory"></param>
        public CourseBasedAuthorizeFilter(EntityType entityType, string entityIdFieldName,
            ICourseReferenceServiceFactory courseReferenceServiceFactory)
        {
            this.entityType = entityType;
            this.courseReferenceServiceFactory = courseReferenceServiceFactory;
            this.entityIdFieldName = entityIdFieldName;
        }

        /// <inheritdoc/>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string currentUserId = context.HttpContext.GetCurrentUserId();
                string objectId = context.HttpContext.Request.RouteValues[entityIdFieldName].ToString();

                var service = courseReferenceServiceFactory.GetByEntityType(entityType);
                string courseId = service.GetCourseIdOf(objectId);

                if (IsAuthorized(currentUserId, courseId, entityType, objectId))
                {
                    // authorization passed -> proceed to controller
                    return;
                }
            }
            context.Result = new UnauthorizedResult();
        }

        /// <summary>
        /// check if the user is authorized to access the course related entity
        /// </summary>
        /// <param name="currentUserId">identifier of the current user</param>
        /// <param name="courseId">identifier of the course</param>
        /// <param name="entityType">type of the selected entity whose id is extracted from the HTTP context</param>
        /// <param name="entityId">identifier of the entity (extracted from HTTP context)</param>
        /// <returns></returns>
        protected abstract bool IsAuthorized(string currentUserId, string courseId, EntityType entityType, string entityId);
    }
}