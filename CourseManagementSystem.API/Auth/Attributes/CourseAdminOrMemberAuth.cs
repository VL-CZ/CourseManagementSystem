using CourseManagementSystem.API.Auth.Factories;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Auth.Attributes
{
    /// <summary>
    /// filter authorizing admin or member of a course that contains the given course-related entity
    /// </summary>
    public class CourseAdminOrMemberAuthorizeFilter : CourseBasedAuthorizeFilter
    {
        private readonly IPeopleService peopleService;

        public CourseAdminOrMemberAuthorizeFilter(EntityType entityType, string entityIdFieldName,
            [FromServices] IPeopleService peopleService, [FromServices] ICourseReferenceServiceFactory courseReferenceServiceFactory)
            : base(entityType, entityIdFieldName, courseReferenceServiceFactory)
        {
            this.peopleService = peopleService;
        }

        /// <inheritdoc/>
        protected override bool IsAuthorized(string currentUserId, string courseId, EntityType entityType, string objectId)
        {
            return peopleService.IsAdminOfCourse(currentUserId, courseId) || peopleService.IsMemberOfCourse(currentUserId, courseId);
        }
    }

    /// <summary>
    /// attribute for authorizing admin or member of a course that contains the given course-related entity
    /// </summary>
    public class AuthorizeCourseAdminOrMemberOfAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// allow course admin or member of a course that contains the selected course-related entity
        /// </summary>
        /// <param name="entityType">type of the selected entity</param>
        /// <param name="entityIdFieldName">name of the field that contains id of the entity</param>
        public AuthorizeCourseAdminOrMemberOfAttribute(EntityType entityType, string entityIdFieldName) : base(typeof(CourseAdminOrMemberAuthorizeFilter))
        {
            Arguments = new object[] { entityType, entityIdFieldName };
        }
    }
}