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
        public TestSubmission CreateEmptySubmission(CourseTest testWithQuestions, CourseMember courseMember)
        {
            var emptyAnswers = testWithQuestions.Questions.Select(
                question => new TestSubmissionAnswer(question, string.Empty));

            return new TestSubmission(testWithQuestions, courseMember,
                emptyAnswers.ToList());
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
        public void MarkAsSubmitted(TestSubmission testSubmission)
        {
            testSubmission.IsSubmitted = true;
        }

        /// <inheritdoc/>
        public void TryToSave(TestSubmission testSubmission)
        {
            DateTime deadline = testSubmission.Test.Deadline;

            // submitted before deadline -> pass
            if (testSubmission.SubmittedDateTime <= deadline)
            {
                dbContext.TestSubmissions.Add(testSubmission);
            }
            else
            {
                throw new ApplicationException("Cannot submit a test after deadline");
            }
        }

        /// <inheritdoc/>
        public void UpdateAnswer(TestSubmissionAnswer answerToEvaluate, int updatedPoints, string updatedComment)
        {
            answerToEvaluate.Points = updatedPoints;
            answerToEvaluate.Comment = updatedComment;
        }

        /// <inheritdoc/>
        public void UpdateAnswerText(TestSubmission testSubmission, int questionNumber, string updatedAnswerText)
        {
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
    }
}