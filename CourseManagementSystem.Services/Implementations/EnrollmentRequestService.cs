using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
using CourseManagementSystem.Services.Interfaces;

namespace CourseManagementSystem.Services.Implementations
{
    public class EnrollmentRequestService : DbService, IEnrollmentRequestService
    {
        public EnrollmentRequestService(CMSDbContext dbContext) : base(dbContext)
        {
        }

        /// <inheritdoc/>
        public void Approve(string requestId)
        {
            // select request and add new course member
            var er = GetById(requestId);
            dbContext.CourseMembers.Add(new CourseMember(er.Person, er.Course));

            DeleteById(requestId);
        }

        /// <inheritdoc/>
        public void Decline(string requestId)
        {
            DeleteById(requestId);
        }

        /// <inheritdoc/>
        public string GetCourseIdOf(string objectId)
        {
            return dbContext.EnrollmentRequests.GetCourseIdOf(objectId);
        }

        /// <summary>
        /// delete enrollment request by its id
        /// </summary>
        /// <param name="requestId">identifier of enrollment request to delete</param>
        private void DeleteById(string requestId)
        {
            var enrollmentRequest = GetById(requestId);
            dbContext.EnrollmentRequests.Remove(enrollmentRequest);
        }

        /// <summary>
        /// get enrollment request by its id
        /// </summary>
        /// <param name="requestId">identifier of enrollment request to get</param>
        private EnrollmentRequest GetById(string requestId)
        {
            return dbContext.EnrollmentRequests.FindById(requestId);
        }
    }
}