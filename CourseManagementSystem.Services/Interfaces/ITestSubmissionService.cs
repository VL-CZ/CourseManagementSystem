using CourseManagementSystem.Data.Models;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ITestSubmissionService : ICourseReferenceService, ICourseMemberReferenceService, IDbService
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
        /// get test submission by its id including the test with questions and answers
        /// </summary>
        /// <param name="testSubmissionId">id of the submission to select</param>
        /// <returns><see cref="TestSubmission"/> with answers and test included</returns>
        TestSubmission GetSubmissionWithTestAndQuestions(string testSubmissionId);

        /// <summary>
        /// get answer to the question with given question number
        /// </summary>
        /// <param name="testSubmission">test submission with answers</param>
        /// <param name="questionNumber">number of the question we search for</param>
        /// <returns>answer to question with given question number</returns>
        TestSubmissionAnswer GetAnswerByQuestionNumber(TestSubmission testSubmission, int questionNumber);

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

        /// <summary>
        /// submit a <see cref="TestSubmission"/>
        /// <br/>
        /// if submitted after deadline, throw <see cref="System.ApplicationException"/>
        /// </summary>
        /// <param name="testSubmission">test submission to submit</param>
        /// <exception cref="System.ApplicationException">if submitted after deadline</exception>
        void Submit(TestSubmission testSubmission);

        /// <summary>
        /// load a test submission related to the given <paramref name="courseMember"/> and <paramref name="courseMember"/>
        /// <br/>
        /// if not present, create a new empty <see cref="TestSubmission"/> 
        /// </summary>
        /// <param name="testWithQuestions">CourseTest that the TestSubmission belongs to</param>
        /// <param name="courseMember">CourseMember that the TestSubmission belongs to</param>
        /// <returns></returns>
        TestSubmission LoadOrCreateSubmission(CourseTest testWithQuestions, CourseMember courseMember);

        /// <summary>
        /// update answer text
        /// </summary>
        /// <param name="testSubmission">test submission that this answer belongs to</param>
        /// <param name="questionNumber">number of question that this answer belongs to</param>
        /// <param name="updatedAnswerText">contents of the answer</param>
        void UpdateAnswerText(TestSubmission testSubmission, int questionNumber, string updatedAnswerText);
    }
}