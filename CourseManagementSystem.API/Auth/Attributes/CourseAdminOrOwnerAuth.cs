using CourseManagementSystem.API.Auth.Factories;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Auth.Attributes
{
    /// <summary>
    /// filter authorizing admin of the course that contains the given course-related entity, or its owner
    /// </summary>
    public class CourseAdminOrOwnerAuthorizeFilter : CourseBasedAuthorizeFilter
    {
        private readonly IPeopleService peopleService;
        private readonly ICourseMemberService courseMemberService;
        private readonly ICourseMemberReferenceServiceFactory courseMemberReferenceServiceFactory;

        public CourseAdminOrOwnerAuthorizeFilter(EntityType entityType, string entityIdFieldName,
            [FromServices] IPeopleService peopleService, [FromServices] ICourseMemberService courseMemberService,
            [FromServices] ICourseReferenceServiceFactory courseReferenceServiceFactory,
            [FromServices] ICourseMemberReferenceServiceFactory courseMemberReferenceServiceFactory)
            : base(entityType, entityIdFieldName, courseReferenceServiceFactory)
        {
            this.peopleService = peopleService;
            this.courseMemberService = courseMemberService;
            this.courseMemberReferenceServiceFactory = courseMemberReferenceServiceFactory;
        }

        /// <inheritdoc/>s
        protected override bool IsAuthorized(string currentUserId, string courseId, EntityType entityType, string objectId)
        {
            var courseMemberRefService = courseMemberReferenceServiceFactory.GetByEntityType(entityType);
            string courseMemberId = courseMemberRefService.GetCourseMemberIdOf(objectId);

            return peopleService.IsAdminOfCourse(currentUserId, courseId) || courseMemberService.BelongsTo(courseMemberId, currentUserId);
        }
    }

    /// <summary>
    /// attribute for authorizing admin of a course that contains the given course-related entity or its owner
    /// </summary>
    public class AuthorizeCourseAdminOrOwnerOfAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// allow only course admin of a course that contains the given course-related entity or its owner
        /// </summary>
        /// <param name="entityType">type of the selected entity</param>
        /// <param name="entityIdFieldName">name of the field that contains id of the entity</param>
        public AuthorizeCourseAdminOrOwnerOfAttribute(EntityType entityType, string entityIdFieldName) : base(typeof(CourseAdminOrOwnerAuthorizeFilter))
        {
            Arguments = new object[] { entityType, entityIdFieldName };
        }
    }
}