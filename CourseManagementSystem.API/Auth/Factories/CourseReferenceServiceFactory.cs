﻿using CourseManagementSystem.Services;
using CourseManagementSystem.Services.Interfaces;
using System.Collections.Generic;

namespace CourseManagementSystem.API.Auth.Factories
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

        public CourseReferenceServiceFactory(ICourseAdminService courseAdminService, ICourseMemberService courseMemberService, ICourseTestService courseTestService,
            IFileService fileService, IForumPostService forumPostService, IGradeService gradeService, ITestSubmissionService testSubmissionService, IEnrollmentRequestService enrollmentRequestService)
        {
            dataServices = new Dictionary<EntityType, ICourseReferenceService>
            {
                [EntityType.Course] = new DummyCourseService(),
                [EntityType.CourseMember] = courseMemberService,
                [EntityType.CourseAdmin] = courseAdminService,
                [EntityType.CourseTest] = courseTestService,
                [EntityType.CourseFile] = fileService,
                [EntityType.ForumPost] = forumPostService,
                [EntityType.Grade] = gradeService,
                [EntityType.TestSubmission] = testSubmissionService,
                [EntityType.EnrollmentRequest] = enrollmentRequestService
            };
        }

        /// <inheritdoc/>
        public ICourseReferenceService GetByEntityType(EntityType entityType)
        {
            return dataServices[entityType];
        }
    }
}