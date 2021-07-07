using CourseManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentRequestsController : ControllerBase
    {
        private readonly IEnrollmentRequestService enrollmentRequestService;

        public EnrollmentRequestsController(IEnrollmentRequestService enrollmentRequestService)
        {
            this.enrollmentRequestService = enrollmentRequestService;
        }

        /// <summary>
        /// approve the enrollment request with the given id
        /// </summary>
        /// <param name="requestId">identifier of the enrollment request to approve</param>
        [HttpPost("approve/{requestId}")]
        public void Approve(string requestId)
        {
            enrollmentRequestService.Approve(requestId);
            enrollmentRequestService.CommitChanges();
        }

        /// <summary>
        /// decline the enrollment request with the given id
        /// </summary>
        /// <param name="requestId">identifier of the enrollment request to decline</param>
        [HttpPost("decline/{requestId}")]
        public void Decline(string requestId)
        {
            enrollmentRequestService.Decline(requestId);
            enrollmentRequestService.CommitChanges();
        }
    }
}