using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            var request = GetReqeustsWithPersonAndCourse()
                        .Single(er => er.Id.ToString() == requestId);
            dbContext.CourseMembers.Add(new CourseMember(request.Person, request.Course));

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

        /// <inheritdoc/>
        public bool HasRequestedEnrollment(string personId, string courseId)
        {
            var requests = GetReqeustsWithPersonAndCourse();
            return requests.Any(req => req.Person.Id.ToString() == personId && req.Course.Id.ToString() == courseId);
        }

        /// <summary>
        /// delete enrollment request by its id
        /// </summary>
        /// <param name="requestId">identifier of enrollment request to delete</param>
        private void DeleteById(string requestId)
        {
            var enrollmentRequest = dbContext.EnrollmentRequests.FindById(requestId);
            dbContext.EnrollmentRequests.Remove(enrollmentRequest);
        }

        /// <summary>
        /// get enrollment requests with person and course objects included
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EnrollmentRequest> GetReqeustsWithPersonAndCourse()
        {
            return dbContext.EnrollmentRequests
                        .Include(er => er.Course)
                        .Include(er => er.Person);
        }
    }
}