using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Extensions;
using CourseManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class TestSubmissionService : DbService, ITestSubmissionService
    {
        public TestSubmissionService(CMSDbContext dbContext) : base(dbContext)
        { }

        /// <inheritdoc/>
        public TestSubmission LoadOrCreateSubmission(CourseTest testWithQuestions, CourseMember courseMember)
        {
            var foundSubmission = TryGetSubmissionByCourseMemberAndTest(courseMember, testWithQuestions);

            // submission not found -> create empty
            if (foundSubmission == null)
                return CreateEmptySubmission(courseMember, testWithQuestions);
            else
                return foundSubmission;
        }

        /// <inheritdoc/>
        public IEnumerable<TestSubmission> GetAllSubmissionsOfCourseMember(string courseMemberId)
        {
            return GetTestSubmissionsWithTestAndStudent().Where(ts => ts.Student.Id.ToString() == courseMemberId);
        }

        /// <inheritdoc/>
        public IEnumerable<TestSubmission> GetAllSubmissionsOfTest(string testId)
        {
            return GetTestSubmissionsWithTestAndStudent().Where(ts => ts.Test.Id.ToString() == testId);
        }

        /// <inheritdoc/>
        public TestSubmissionAnswer GetAnswerByQuestionNumber(TestSubmission testSubmission, int questionNumber)
        {
            return testSubmission.Answers.SingleOrDefault(answer => answer.Question.Number == questionNumber);
        }

        /// <inheritdoc/>
        public string GetCourseIdOf(string objectId)
        {
            string courseMemberId = GetCourseMemberIdOf(objectId);
            return dbContext.CourseMembers.GetCourseIdOf(courseMemberId);
        }

        /// <inheritdoc/>
        public string GetCourseMemberIdOf(string objectId)
        {
            return dbContext.TestSubmissions.GetCourseMemberIdOf(objectId);
        }

        /// <inheritdoc/>
        public TestSubmission GetSubmissionWithTestAndQuestions(string testSubmissionId)
        {
            return dbContext.TestSubmissions
                .Include(ts => ts.Answers)
                .ThenInclude(a => a.Question)
                .Include(ts => ts.Test)
                .SingleOrDefault(ts => ts.Id.ToString() == testSubmissionId);
        }

        /// <inheritdoc/>
        public void MarkAsReviewed(TestSubmission testSubmission)
        {
            testSubmission.IsReviewed = true;
        }

        /// <inheritdoc/>
        public void Submit(TestSubmission testSubmission)
        {
            CheckIfNotSubmitted(testSubmission);

            DateTime testDeadline = testSubmission.Test.Deadline;
            DateTime currentDateTime = DateTime.UtcNow;

            // submitted before deadline -> pass
            if (currentDateTime <= testDeadline)
            {
                testSubmission.SubmittedDateTime = currentDateTime;
                testSubmission.IsSubmitted = true;
            }
            else
            {
                throw new ApplicationException("Cannot submit a test after deadline");
            }
        }

        /// <inheritdoc/>
        public void UpdateAnswerProperties(TestSubmissionAnswer answerToEvaluate, int updatedPoints, string updatedComment)
        {
            answerToEvaluate.Points = updatedPoints;
            answerToEvaluate.Comment = updatedComment;
        }

        /// <inheritdoc/>
        public void UpdateAnswerText(TestSubmission testSubmission, int questionNumber, string updatedAnswerText)
        {
            CheckIfNotSubmitted(testSubmission);

            var answer = GetAnswerByQuestionNumber(testSubmission, questionNumber);
            answer.Text = updatedAnswerText;
        }

        /// <summary>
        /// get all test submissions with test, student, user, answer and question loaded
        /// </summary>
        /// <returns>test submissions with test, student, user, answer and question included</returns>
        private IEnumerable<TestSubmission> GetTestSubmissionsWithTestAndStudent()
        {
            return dbContext.TestSubmissions.Include(ts => ts.Test)
                .Include(ts => ts.Student).ThenInclude(stud => stud.User)
                .Include(ts => ts.Answers).ThenInclude(ans => ans.Question);
        }

        /// <summary>
        /// try to get <see cref="TestSubmission"/> that belongs to the given <see cref="CourseMember"/> and <see cref="CourseTest"/>
        /// </summary>
        /// <param name="courseMember">given <see cref="CourseMember"/> of the test submission</param>
        /// <param name="courseTest">given <see cref="CourseTest"/> of the test submission</param>
        /// <returns><see cref="TestSubmission"/> if exists, otherwise NULL</returns>
        private TestSubmission TryGetSubmissionByCourseMemberAndTest(CourseMember courseMember, CourseTest courseTest)
        {
            return GetAllSubmissionsOfCourseMember(courseMember.Id.ToString())
                .SingleOrDefault(ts => ts.Test.Id == courseTest.Id);
        }

        /// <summary>
        /// create new empty <see cref="TestSubmission"/>
        /// </summary>
        /// <param name="courseMember"><see cref="CourseMember"/> of the test submission</param>
        /// <param name="testWithQuestions">given <see cref="CourseTest"/> of the test submission</param>
        /// <returns>empty <see cref="TestSubmission"/></returns>
        private TestSubmission CreateEmptySubmission(CourseMember courseMember, CourseTest testWithQuestions)
        {
            var emptyAnswers = testWithQuestions.Questions.Select(
                question => new TestSubmissionAnswer(question, string.Empty));

            var emptyTestSubmission = new TestSubmission(testWithQuestions, courseMember,
                emptyAnswers.ToList());
            dbContext.TestSubmissions.Add(emptyTestSubmission);

            return emptyTestSubmission;
        }

        /// <summary>
        /// check whether the test submission hasn't been submitted yet
        /// <br/>
        /// throws exception if already submitted
        /// </summary>
        /// <param name="testSubmission">test submission to check</param>
        /// <exception cref="ApplicationException">if already submitted</exception>
        private void CheckIfNotSubmitted(TestSubmission testSubmission)
        {
            if (testSubmission.IsSubmitted)
            {
                throw new ApplicationException("The test is already submitted");
            }
        }
    }
}