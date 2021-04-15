using CourseManagementSystem.Services.Interfaces;
using System.Collections.Generic;

namespace CourseManagementSystem.API.Auth.Factories
{
    /// <summary>
    /// interface for <see cref="ICourseMemberReferenceService"/> factory
    /// </summary>
    public interface ICourseMemberReferenceServiceFactory
    {
        /// <summary>
        /// get service that belongs to the given entity type
        /// </summary>
        /// <param name="entityType">type of entity that the service belongs to</param>
        /// <returns>service that belongs </returns>
        ICourseMemberReferenceService GetByEntityType(EntityType entityType);
    }

    /// <summary>
    /// factory for <see cref="ICourseReferenceService"/>
    /// </summary>
    public class CourseMemberReferenceServiceFactory : ICourseMemberReferenceServiceFactory
    {
        /// <summary>
        /// dummy implementation of course service (necessary because <see cref="ICourseTestService"/> doesn't inherit form <see cref="ICourseReferenceService"/>
        /// </summary>
        private class DummyCourseMemberService : ICourseMemberReferenceService
        {
            /// <inheritdoc/>
            public string GetCourseMemberIdOf(string objectId)
            {
                return objectId;
            }
        }

        /// <summary>
        /// dictionary of data services where the key is <see cref="EntityType"/>
        /// </summary>
        private readonly IReadOnlyDictionary<EntityType, ICourseMemberReferenceService> dataServices;

        public CourseMemberReferenceServiceFactory(ITestSubmissionService testSubmissionService)
        {
            dataServices = new Dictionary<EntityType, ICourseMemberReferenceService>
            {
                [EntityType.CourseMember] = new DummyCourseMemberService(),
                [EntityType.TestSubmission] = testSubmissionService
            };
        }

        /// <inheritdoc/>
        public ICourseMemberReferenceService GetByEntityType(EntityType entityType)
        {
            return dataServices[entityType];
        }
    }
}