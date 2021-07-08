namespace CourseManagementSystem.API.Auth
{
    /// <summary>
    /// enum that represents types of entities in the app 
    /// </summary>
    public enum EntityType
    {
        /// <summary>
        /// <see cref="Data.Models.CourseAdmin"/> entity type
        /// </summary>
        CourseAdmin,

        /// <summary>
        /// <see cref="Data.Models.CourseMember"/> entity type
        /// </summary>
        CourseMember,

        /// <summary>
        /// <see cref="Data.Models.Course"/> entity type
        /// </summary>
        Course,

        /// <summary>
        /// <see cref="Data.Models.CourseTest"/> entity type
        /// </summary>
        CourseTest,

        /// <summary>
        /// <see cref="Data.Models.CourseFile"/> entity type
        /// </summary>
        CourseFile,

        /// <summary>
        /// <see cref="Data.Models.ForumPost"/> entity type
        /// </summary>
        ForumPost,

        /// <summary>
        /// <see cref="Data.Models.Grade"/> entity type
        /// </summary>
        Grade,

        /// <summary>
        /// <see cref="Data.Models.TestSubmission"/> entity type
        /// </summary>
        TestSubmission,

        /// <summary>
        /// <see cref="Data.Models.EnrollmentRequest"/> entity type
        /// </summary>
        EnrollmentRequest
    }
}