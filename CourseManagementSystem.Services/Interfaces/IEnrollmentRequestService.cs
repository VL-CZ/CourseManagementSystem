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
    }
}