using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// class representing basic info about the test submission
    /// </summary>
    public class TestSubmissionInfoVM
    {
        public TestSubmissionInfoVM(int testSubmissoinId, string testTopic)
        {
            TestSubmissoinId = testSubmissoinId;
            TestTopic = testTopic;
        }

        /// <summary>
        /// id of the test submission
        /// </summary>
        public int TestSubmissoinId { get; set; }

        /// <summary>
        /// topic of the test
        /// </summary>
        public string TestTopic { get; set; }
    }
}
