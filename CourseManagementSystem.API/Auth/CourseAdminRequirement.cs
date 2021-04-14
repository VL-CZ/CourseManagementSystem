﻿using CourseManagementSystem.API.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CourseManagementSystem.API.Auth
{
    /// <summary>
    /// filter authorizing admin of a course
    /// </summary>
    public class CourseAdminAuthorizeFilter : IAuthorizationFilter
    {
        private readonly EntityType entityType;
        private readonly IPeopleService peopleService;
        private readonly ICourseReferenceServiceFactory courseReferenceServiceFactory;
        private readonly string idFieldName;

        public CourseAdminAuthorizeFilter(EntityType entityType, string entityIdFieldName,
            [FromServices] IPeopleService peopleService, [FromServices] ICourseReferenceServiceFactory courseReferenceServiceFactory)
        {
            this.entityType = entityType;
            this.peopleService = peopleService;
            this.courseReferenceServiceFactory = courseReferenceServiceFactory;
            this.idFieldName = entityIdFieldName;
        }

        /// <inheritdoc/>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string currentUserId = context.HttpContext.GetCurrentUserId();
                string objectId = context.HttpContext.Request.RouteValues[idFieldName].ToString();

                var service = courseReferenceServiceFactory.GetByEntityType(entityType);
                string courseId = service.GetCourseIdOf(objectId);

                if (peopleService.IsAdminOfCourse(currentUserId, courseId))
                {
                    // authorization passed -> proceed to controller
                    return;
                }
            }
            context.Result = new UnauthorizedResult();
        }
    }

    /// <summary>
    /// allow only course admin
    /// </summary>
    public class AllowCourseAdminOfAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// allow only course admin of a selected entity
        /// </summary>
        /// <param name="entityType">type of the selected entity</param>
        /// <param name="entityIdFieldName">name of the field that contains id of the entity</param>
        public AllowCourseAdminOfAttribute(EntityType entityType, string entityIdFieldName) : base(typeof(CourseAdminAuthorizeFilter))
        {
            Arguments = new object[] { entityType, entityIdFieldName };
        }
    }
}