namespace CourseManagementSystem.Services.Interfaces
{
    public interface IEnrollmentRequestService : ICourseReferenceService, IDbService
    {
        /// <summary>
        /// approve the enrollment request with the given id
        /// </summary>
        /// <param name="requestId">identifier of the selected enrollment request</param>
        void Approve(string requestId);

        /// <summary>
        /// decline the enrollment request with the given id
        /// </summary>
        /// <param name="requestId">identifier of the selected enrollment request</param>
        void Decline(string requestId);

        /// <summary>
        /// check if the person has already reqeusted access to the course
        /// </summary>
        /// <param name="personId">identifier of the person</param>
        /// <param name="courseId">identifier of the course</param>
        bool HasRequestedEnrollment(string personId, string courseId);
    }
}