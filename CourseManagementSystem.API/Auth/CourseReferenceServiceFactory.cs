using CourseManagementSystem.Services.Interfaces;
using System.Collections.Generic;

namespace CourseManagementSystem.API.Auth
{
    /// <summary>
    /// interface for <see cref="ICourseReferenceService"/> factory
    /// </summary>
    public interface ICourseReferenceServiceFactory
    {
        /// <summary>
        /// get service that belongs to the given entity type
        /// </summary>
        /// <param name="entityType">type of entity that the service belongs to</param>
        /// <returns>service that belongs </returns>
        ICourseReferenceService GetByEntityType(EntityType entityType);
    }

    /// <summary>
    /// factory for <see cref="ICourseReferenceService"/>
    /// </summary>
    public class CourseReferenceServiceFactory : ICourseReferenceServiceFactory
    {
        /// <summary>
        /// dummy implementation of course service (necessary because <see cref="ICourseTestService"/> doesn't inherit form <see cref="ICourseReferenceService"/>
        /// </summary>
        private class DummyCourseService : ICourseReferenceService
        {
            /// <inheritdoc/>
            public string GetCourseIdOf(string objectId)
            {
                return objectId;
            }
        }

        /// <summary>
        /// dictionary of data services where the key is <see cref="EntityType"/>
        /// </summary>
        private readonly IReadOnlyDictionary<EntityType, ICourseReferenceService> dataServices;

        public CourseReferenceServiceFactory(ICourseMemberService courseMemberService, ICourseTestService courseTestService,
            IFileService fileService, IForumPostService forumPostService, IGradeService gradeService, ITestSubmissionService testSubmissionService)
        {
            dataServices = new Dictionary<EntityType, ICourseReferenceService>
            {
                [EntityType.Course] = new DummyCourseService(),
                [EntityType.CourseMember] = courseMemberService,
                [EntityType.CourseTest] = courseTestService,
                [EntityType.CourseFile] = fileService,
                [EntityType.ForumPost] = forumPostService,
                [EntityType.Grade] = gradeService,
                [EntityType.TestSubmission] = testSubmissionService
            };
        }

        /// <inheritdoc/>
        public ICourseReferenceService GetByEntityType(EntityType authEnum)
        {
            return dataServices[authEnum];
        }
    }
}