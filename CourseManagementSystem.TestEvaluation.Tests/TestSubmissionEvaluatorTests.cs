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
        private const string answersDelimiter = "||";

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

        [Fact]
        public void Evaluate_SingleChoiceAnswers()
        {
            questions.Clear();
            questions.AddRange(new List<TestQuestion>()
            {
                new TestQuestion(1,"What's the capital city of Germany? ||A|Munich||B|Berlin","B",2, Data.QuestionType.SingleChoice),
                new TestQuestion(2,"What's the capital city of Czechia? ||A|Praha||B|Brno||C|Ostrava","A",5, Data.QuestionType.SingleChoice)
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

        [Fact]
        public void Evaluate_MultipleChoiceAnswers()
        {
            questions.Clear();
            questions.AddRange(new List<TestQuestion>()
            {
                new TestQuestion(1,"Which of the following cities are located in Germany? ||A|Munich||B|Amsterdam||C|Vienna||D|Dresden","A||D",4, Data.QuestionType.MultipleChoice),
                new TestQuestion(1,"Which of the following cities are located in Czechia? ||A|Brno||B|Prague||C|Warsaw","B||A",4, Data.QuestionType.MultipleChoice),
            });

            submissionToEvaluate.Answers = new List<TestSubmissionAnswer>
            {
                new TestSubmissionAnswer(questions[0], "A||B"),
                new TestSubmissionAnswer(questions[1], "A||B||C")
            };

            testSubmissionEvaluator.Evaluate(submissionToEvaluate);

            var answers = submissionToEvaluate.Answers.ToList();

            Assert.Equal(2, answers[0].Points);
            Assert.Equal(0, answers[1].Points);
        }
    }
}