using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ITestSubmissionService
    {
        /// <summary>
        /// get all submissions of the given <see cref="CourseMember"/>
        /// </summary>
        /// <param name="courseMemberId">id of the <see cref="CourseMember"/></param>
        /// <returns>all test submissions of the <see cref="CourseMember"/></returns>
        IEnumerable<TestSubmission> GetAllSubmissionsOfCourseMember(int courseMemberId);

        /// <summary>
        /// get all test submissions that belong to given test
        /// </summary>
        /// <param name="testId">ID of the test</param>
        /// <returns></returns>
        IEnumerable<TestSubmission> GetAllSubmissionsOfTest(int testId);

        /// <summary>
        /// get test submission (with answers and test included) by its id
        /// </summary>
        /// <param name="testSubmissionId">id of the submission to select</param>
        /// <returns><see cref="TestSubmission"/> with answers and test included</returns>
        TestSubmission GetSubmissionById(int testSubmissionId);

        /// <summary>
        /// get answer to the question with given question number
        /// </summary>
        /// <param name="testSubmission">test submission with answers</param>
        /// <param name="questionNumber">number of the question we search for</param>
        /// <returns>answer to question with given question number</returns>
        TestSubmissionAnswer GetAnswerByQuestionNumber(TestSubmission testSubmission, int questionNumber);
    }
}
