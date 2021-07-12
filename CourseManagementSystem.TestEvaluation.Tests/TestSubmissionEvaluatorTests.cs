using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CourseManagementSystem.TestEvaluation.Tests
{
    public class TestSubmissionEvaluatorTests
    {
        private List<TestQuestion> questions;
        private TestSubmission submissionToEvaluate;
        private TestSubmissionEvaluator testSubmissionEvaluator;
        private const string acd = QuestionWithChoicesTools.answerChoicesDelimiter;
        private const string ltd = QuestionWithChoicesTools.answerChoiceLetterAndTextDelimiter;
        private const string ald = QuestionWithChoicesTools.answerLetterDelimiter;

        public TestSubmissionEvaluatorTests()
        {
            testSubmissionEvaluator = new TestSubmissionEvaluator();

            questions = new List<TestQuestion>()
            {
                new TestQuestion(1, "What's the capital city of Russia?", "Moscow", 3, Data.QuestionType.TextAnswer),
                new TestQuestion(2, "What's the capital city of Japan?", "Tokyo", 2, Data.QuestionType.TextAnswer),
                new TestQuestion(3, "What's the capital city of the UK?", "London", 1, Data.QuestionType.TextAnswer),
            };

            var courseTest = new CourseTest(string.Empty, questions, 0, DateTime.Now, true);
            submissionToEvaluate = new TestSubmission(courseTest, null, new List<TestSubmissionAnswer>());
        }

        /// <summary>
        /// test Evaluate method on test submission that has both correct and wrong answers
        /// </summary>
        [Fact]
        public void Evaluate_CorrectAndWrong()
        {
            submissionToEvaluate.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(questions[0], "Moscow"),
                new TestSubmissionAnswer(questions[1], "Prague"),
                new TestSubmissionAnswer(questions[2], "London"),
            };

            testSubmissionEvaluator.Evaluate(submissionToEvaluate);

            var answers = submissionToEvaluate.Answers.ToList();

            Assert.Equal(3, answers[0].Points);
            Assert.Equal(0, answers[1].Points);
            Assert.Equal(1, answers[2].Points);
        }

        /// <summary>
        /// test Evaluate method on test submission that has only correct answers
        /// </summary>
        [Fact]
        public void Evaluate_AllCorrect()
        {
            submissionToEvaluate.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(questions[0], "Moscow"),
                new TestSubmissionAnswer(questions[1], "Tokyo"),
                new TestSubmissionAnswer(questions[2], "London"),
            };

            testSubmissionEvaluator.Evaluate(submissionToEvaluate);

            var answers = submissionToEvaluate.Answers.ToList();

            Assert.Equal(3, answers[0].Points);
            Assert.Equal(2, answers[1].Points);
            Assert.Equal(1, answers[2].Points);
        }

        /// <summary>
        /// test Evaluate method on test submission that has only wrong answers
        /// </summary>
        [Fact]
        public void Evaluate_AllWrong()
        {
            submissionToEvaluate.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(questions[0], "Stockholm"),
                new TestSubmissionAnswer(questions[1], ""),
                new TestSubmissionAnswer(questions[2], null),
            };

            testSubmissionEvaluator.Evaluate(submissionToEvaluate);

            var answers = submissionToEvaluate.Answers.ToList();

            Assert.Equal(0, answers[0].Points);
            Assert.Equal(0, answers[1].Points);
            Assert.Equal(0, answers[2].Points);
        }

        /// <summary>
        /// test Evaluate method on the test with single choice questions
        /// </summary>
        [Fact]
        public void Evaluate_SingleChoiceAnswers()
        {
            questions.Clear();
            questions.AddRange(new List<TestQuestion>()
            {
                new TestQuestion(1,$"What's the capital city of Germany? {acd}A{ltd}Munich{acd}B{ltd}Berlin","B",2, Data.QuestionType.SingleChoice),
                new TestQuestion(2,$"What's the capital city of Czechia? {acd}A{ltd}Praha{acd}B{ltd}Brno{acd}C{ltd}Ostrava",
                    "A",5, Data.QuestionType.SingleChoice)
            });

            submissionToEvaluate.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(questions[0], "B"),
                new TestSubmissionAnswer(questions[1], "C")
            };

            testSubmissionEvaluator.Evaluate(submissionToEvaluate);

            var answers = submissionToEvaluate.Answers.ToList();

            Assert.Equal(2, answers[0].Points);
            Assert.Equal(0, answers[1].Points);
        }

        /// <summary>
        /// test Evaluate method on the test with multiple choice questions
        /// </summary>
        [Fact]
        public void Evaluate_MultipleChoiceAnswers()
        {
            questions.Clear();
            questions.AddRange(new List<TestQuestion>()
            {
                new TestQuestion(1,
                    $"Which of the following cities are located in Germany? {acd}A{ltd}Munich{acd}B{ltd}Amsterdam{acd}C{ltd}Vienna{acd}D{ltd}Dresden",
                    $"A{ald}D", 4, Data.QuestionType.MultipleChoice),
                new TestQuestion(2,
                    $"Which of the following cities are located in Czechia? {acd}A{ltd}Brno{acd}B{ltd}Prague{acd}C{ltd}Warsaw",
                    $"B{ald}A", 4, Data.QuestionType.MultipleChoice),
                new TestQuestion(3,
                    $"Which of these languages support object oriented programming? {acd}A{ltd}Java{acd}B{ltd}C#",
                    $"A{ald}B", 2, Data.QuestionType.MultipleChoice),
                new TestQuestion(3,
                    $"Which of these languages are functional? {acd}A{ltd}Haskell{acd}B{ltd}F#",
                    $"A{ald}B", 2, Data.QuestionType.MultipleChoice),
            });

            submissionToEvaluate.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(questions[0], $"A{ald}B"),
                new TestSubmissionAnswer(questions[1], $"A{ald}B{ald}C"),
                new TestSubmissionAnswer(questions[2], $"B{ald}A"),
                new TestSubmissionAnswer(questions[3], "")
            };

            testSubmissionEvaluator.Evaluate(submissionToEvaluate);

            var answers = submissionToEvaluate.Answers.ToList();

            Assert.Equal(2, answers[0].Points);
            Assert.Equal(3, answers[1].Points);
            Assert.Equal(2, answers[2].Points);
            Assert.Equal(0, answers[3].Points);
        }
    }
}