using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using CourseManagementSystem.Services.Interfaces;
using CourseManagementSystem.TestEvaluation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Controllers
{
    /// <summary>
    /// this controller is used for generating test data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        private readonly CMSDbContext dbContext;
        private readonly IPeopleService peopleService;
        private readonly TestSubmissionEvaluator testSubmissionEvaluator = new TestSubmissionEvaluator();
        private const string acd = QuestionWithChoicesTools.answerChoicesDelimiter;
        private const string ltd = QuestionWithChoicesTools.answerChoiceLetterAndTextDelimiter;
        private const string ald = QuestionWithChoicesTools.answerLetterDelimiter;
        private readonly DateTime endOfYear2021 = new DateTime(2021, 12, 31, 12, 0, 0);

        public TestDataController(CMSDbContext dbContext, IPeopleService peopleService)
        {
            this.dbContext = dbContext;
            this.peopleService = peopleService;
        }

        /// <summary>
        /// insert test data into the database
        /// this method is only for testing purposes
        /// in production, we need to comment/remove it!
        /// </summary>
        /// <param name="person1Id"></param>
        /// <param name="person2Id"></param>
        /// <param name="person3Id"></param>
        /// <param name="person4Id"></param>
        [HttpPost("generate/{person1Id}/{person2Id}/{person3Id}/{person4Id}")]
        public void GenerateTestData(string person1Id, string person2Id, string person3Id, string person4Id)
        {
            var person1 = peopleService.GetById(person1Id);
            var person2 = peopleService.GetById(person2Id);
            var person3 = peopleService.GetById(person3Id);
            var person4 = peopleService.GetById(person4Id);

            // create courses

            var programmingCourse = new Course("Programming", person1);
            var geographyCourse = new Course("Geography", person2);

            dbContext.Courses.Add(programmingCourse);
            dbContext.Courses.Add(geographyCourse);

            // create tests

            var geographyTestQuestions = new List<TestQuestion>()
            {
                new TestQuestion(1,$"What's the capital city of Germany?","Berlin",3, QuestionType.TextAnswer),
                new TestQuestion(2,$"Which of these cities are located in Czechia? {acd}A{ltd}Praha{acd}B{ltd}Brno{acd}C{ltd}Vienna",
                    "A,B",6, QuestionType.MultipleChoice)
            };
            var geographyTest = new CourseTest("Geography of Europe", geographyTestQuestions, 5, endOfYear2021, true)
            {
                Status = TestStatus.Published
            };
            geographyCourse.Tests.Add(geographyTest);

            var geographyQuiz = new CourseTest("Quiz - Czechia",
                new List<TestQuestion> { new TestQuestion(1, "What's the capital of Czechia?", "Prague", 5, QuestionType.TextAnswer) },
                1, endOfYear2021, false);
            geographyCourse.Tests.Add(geographyQuiz);

            // --------------------------

            var programmingTestQuestions = new List<TestQuestion> {
                new TestQuestion(1,
                    $"Is Java language statically typed? {acd}A{ltd}Yes{acd}B{ltd}No",
                    "A", 1, QuestionType.SingleChoice),
                new TestQuestion(2,
                    $"Which of these languages support functional programming? {acd}A{ltd}Haskell{acd}B{ltd}F#",
                    $"A{ald}B", 4, QuestionType.MultipleChoice)
            };
            var programmingTest = new CourseTest("Programming languages test", programmingTestQuestions, 5, endOfYear2021, true) { Status = TestStatus.Published };
            programmingCourse.Tests.Add(programmingTest);

            var dotnetTest = new CourseTest(".NET CLI test",
                new List<TestQuestion> { new TestQuestion(1, "What command do we use for building .NET Core app from the command line?", "dotnet build", 2, QuestionType.TextAnswer) },
                2, endOfYear2021, true)
            { Status = TestStatus.Published };
            programmingCourse.Tests.Add(dotnetTest);

            // create course members

            var person1geographyCourse = new CourseMember(person1, geographyCourse);
            var person3programmingCourse = new CourseMember(person3, programmingCourse);
            var person4geographyCourse = new CourseMember(person4, geographyCourse);

            dbContext.CourseMembers.Add(person1geographyCourse);
            dbContext.CourseMembers.Add(person3programmingCourse);
            dbContext.CourseMembers.Add(person4geographyCourse);

            // create enrollment requests

            var person4programmingCourse = new EnrollmentRequest(programmingCourse, person4);
            dbContext.EnrollmentRequests.Add(person4programmingCourse);

            // create posts

            dbContext.Posts.Add(new ForumPost("Hello everyone, welcome to Programming course!", person1, programmingCourse));

            // create grades

            dbContext.Grades.Add(new Grade(1, "", "Activity 20.6.", 1, person1geographyCourse));
            dbContext.Grades.Add(new Grade(1, "", "Points for activity", 1, person3programmingCourse));

            // create test submissions

            var submittedAnswers1 = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(geographyTestQuestions[0],"Berlin"),
                new TestSubmissionAnswer(geographyTestQuestions[1],$"A{ald}B{ald}C")
            };
            var geographyTestPerson1 = new TestSubmission(geographyTest, person1geographyCourse, submittedAnswers1) { IsSubmitted = true, SubmittedDateTime = DateTime.UtcNow };

            var submittedAnswers4 = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(geographyTestQuestions[0],"Hamburg"),
                new TestSubmissionAnswer(geographyTestQuestions[1],$"A{ald}B{ald}")
            };
            var geographyTestPerson4 = new TestSubmission(geographyTest, person4geographyCourse, submittedAnswers4) { IsSubmitted = true, SubmittedDateTime = DateTime.UtcNow };

            var submittedAnswers31 = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(programmingTestQuestions[0],"B"),
                new TestSubmissionAnswer(programmingTestQuestions[1],$"A{ald}B")
            };
            var programmingTestPerson3 = new TestSubmission(programmingTest, person3programmingCourse, submittedAnswers31) { IsSubmitted = true, SubmittedDateTime = DateTime.UtcNow };

            var submittedAnswers32 = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(dotnetTest.Questions.First(),"F5 in Visual Studio"){ Comment="The question was about the build from command line." }
            };
            var dotnetTestPerson3 = new TestSubmission(dotnetTest, person3programmingCourse, submittedAnswers32) { IsSubmitted = true, IsReviewed = true };

            var allSubmissions = new List<TestSubmission> { geographyTestPerson1, geographyTestPerson4, programmingTestPerson3, dotnetTestPerson3 };
            allSubmissions.ForEach(submission => testSubmissionEvaluator.Evaluate(submission));

            dbContext.TestSubmissions.AddRange(allSubmissions);

            // save the changes

            dbContext.SaveChanges();
        }
    }
}