using CourseManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSubmissionsController : ControllerBase
    {
        /// <summary>
        /// submit a test into selected course
        /// </summary>
        /// <param name="test"></param>
        /// <param name="courseId"></param>
        [HttpPost("{courseId}")]
        public void Submit(TestSubmission submission, int courseId)
        {

        }

        /// <summary>
        /// get given test submission
        /// </summary>
        /// <param name="testSubmissionId"></param>
        /// <returns></returns>
        [HttpGet("{testSubmissionId}")]
        public TestSubmission Get(int testSubmissionId)
        {
            return null;
        }
    }
}