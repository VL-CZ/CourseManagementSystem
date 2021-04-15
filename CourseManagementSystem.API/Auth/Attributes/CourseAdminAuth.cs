using CourseManagementSystem.API.Auth.Factories;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Auth.Attributes
{
    /// <summary>
    /// filter authorizing admin of a course that contains the given course-related entity
    /// </summary>
    public class CourseAdminAuthorizeFilter : CourseBasedAuthorizeFilter
    {
        private readonly IPeopleService peopleService;

        public CourseAdminAuthorizeFilter(EntityType entityType, string entityIdFieldName,
            [FromServices] IPeopleService peopleService, [FromServices] ICourseReferenceServiceFactory courseReferenceServiceFactory)
            : base(entityType, entityIdFieldName, courseReferenceServiceFactory)
        {
            this.peopleService = peopleService;
        }

        /// <inheritdoc/>
        protected override bool IsAuthorized(string currentUserId, string courseId, EntityType entityType, string objectId)
        {
            return peopleService.IsAdminOfCourse(currentUserId, courseId);
        }
    }

    /// <summary>
    /// attribute for authorizing admin of a course
    /// </summary>
    public class AuthorizeCourseAdminOfAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// allow only course admin of a selected course-related entity
        /// </summary>
        /// <param name="entityType">type of the selected entity</param>
        /// <param name="entityIdFieldName">name of the field that contains id of the entity</param>
        public AuthorizeCourseAdminOfAttribute(EntityType entityType, string entityIdFieldName) : base(typeof(CourseAdminAuthorizeFilter))
        {
            Arguments = new object[] { entityType, entityIdFieldName };
        }
    }
}