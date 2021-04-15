using CourseManagementSystem.Data.Models;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ITestSubmissionService : ICourseReferenceService, ICourseMemberReferenceService
    {
        /// <summary>
        /// get all submissions of the given <see cref="CourseMember"/>
        /// </summary>
        /// <param name="courseMemberId">id of the <see cref="CourseMember"/></param>
        /// <returns>all test submissions of the <see cref="CourseMember"/></returns>
        IEnumerable<TestSubmission> GetAllSubmissionsOfCourseMember(string courseMemberId);

        /// <summary>
        /// get all test submissions that belong to given test
        /// </summary>
        /// <param name="testId">ID of the test</param>
        /// <returns></returns>
        IEnumerable<TestSubmission> GetAllSubmissionsOfTest(string testId);

        /// <summary>
        /// get test submission (with answers and test included) by its id
        /// </summary>
        /// <param name="testSubmissionId">id of the submission to select</param>
        /// <returns><see cref="TestSubmission"/> with answers and test included</returns>
        TestSubmission GetSubmissionById(string testSubmissionId);

        /// <summary>
        /// get answer to the question with given question number
        /// </summary>
        /// <param name="testSubmission">test submission with answers</param>
        /// <param name="questionNumber">number of the question we search for</param>
        /// <returns>answer to question with given question number</returns>
        TestSubmissionAnswer GetAnswerByQuestionNumber(TestSubmission testSubmission, int questionNumber);

        /// <summary>
        /// save a test submission
        /// </summary>
        /// <param name="testSubmission">test submission to save</param>
        void Save(TestSubmission testSubmission);

        /// <summary>
        /// update properties of the answer
        /// </summary>
        /// <param name="answer">answer to update</param>
        /// <param name="updatedPoints">updated value of <see cref="TestSubmissionAnswer.Points"/></param>
        /// <param name="updatedComment">updated value of <see cref="TestSubmissionAnswer.Comment"/></param>
        void UpdateAnswer(TestSubmissionAnswer answer, int updatedPoints, string updatedComment);

        /// <summary>
        /// mark the test submission as reviewed
        /// </summary>
        /// <param name="testSubmission">reviewed test submission</param>
        void MarkAsReviewed(TestSubmission testSubmission);
    }
}