using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Tools
{
    /// <summary>
    /// helper class for filtering the <see cref="CourseTest"/> objects
    /// </summary>
    public class CourseTestFilter
    {
        /// <summary>
        /// filter active tests from <paramref name="tests"/>
        /// <br/>
        /// a test is active if it's published and before deadline
        /// </summary>
        /// <param name="tests">tests to filter</param>
        /// <returns></returns>
        public IEnumerable<CourseTest> FilterActive(IEnumerable<CourseTest> tests)
        {
            return tests
                .Where(test => IsPublished(test))
                .Where(test => test.Deadline > DateTime.UtcNow);
        }

        /// <summary>
        /// filter tests that haven't been published yet from <paramref name="tests"/>
        /// </summary>
        /// <param name="tests">tests to filter</param>
        /// <returns></returns>
        public IEnumerable<CourseTest> FilterNonPublished(IEnumerable<CourseTest> tests)
        {
            return tests
                .Where(test => !IsPublished(test));
        }

        /// <summary>
        /// filter tests that are after deadline from <paramref name="tests"/>
        /// </summary>
        /// <param name="tests">tests to filter</param>
        /// <returns></returns>
        public IEnumerable<CourseTest> FilterAfterDeadline(IEnumerable<CourseTest> tests)
        {
            return tests
                .Where(test => test.Deadline < DateTime.UtcNow);
        }

        /// <summary>
        /// check if the test is published
        /// </summary>
        /// <param name="test">test to check</param>
        /// <returns></returns>
        private bool IsPublished(CourseTest test)
        {
            return test.Status == TestStatus.Published;
        }
    }
}