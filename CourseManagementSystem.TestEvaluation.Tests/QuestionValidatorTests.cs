using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using Xunit;

namespace CourseManagementSystem.TestEvaluation.Tests
{
    /// <summary>
    /// test class for <see cref="QuestionValidator"/> class
    /// </summary>
    public class QuestionValidatorTests
    {
        private const string acd = QuestionWithChoicesTools.answerChoicesDelimiter;
        private const string ltd = QuestionWithChoicesTools.answerChoiceLetterAndTextDelimiter;
        private const string ald = QuestionWithChoicesTools.answerLetterDelimiter;

        private readonly QuestionValidator questionValidator;

        public QuestionValidatorTests()
        {
            questionValidator = new QuestionValidator();
        }

        /// <summary>
        /// test HasValidCorrectAnswer on a question with textual answer
        /// </summary>
        [Fact]
        public void HasValidCorrectAnswer_TextAnswer_Valid()
        {
            var question = new TestQuestion(1, "Q1", "A1", 2, QuestionType.TextAnswer);

            bool valid = questionValidator.HasValidCorrectAnswer(question);
            
            Assert.True(valid);
        }

        /// <summary>
        /// test HasValidCorrectAnswer on a single choice question with valid correct answers
        /// </summary>
        [Fact]
        public void HasValidCorrectAnswer_SingleChoice_Valid()
        {
            var question = new TestQuestion(1, $"What's the capital city of Germany? {acd}A{ltd}Munich{acd}B{ltd}Berlin", "B", 2, QuestionType.SingleChoice);

            bool valid = questionValidator.HasValidCorrectAnswer(question);

            Assert.True(valid);
        }

        /// <summary>
        /// test HasValidCorrectAnswer on a single choice question with invalid letter in correct answer
        /// </summary>
        [Fact]
        public void HasValidCorrectAnswer_SingleChoice_InvalidLetter()
        {
            var question = new TestQuestion(1, $"What's the capital city of Germany? {acd}A{ltd}Munich{acd}B{ltd}Berlin", "D", 2, QuestionType.SingleChoice);

            bool valid = questionValidator.HasValidCorrectAnswer(question);

            Assert.False(valid);
        }

        /// <summary>
        /// test HasValidCorrectAnswer on a single choice question with multiple letter in correct answers -> FAIL
        /// </summary>
        [Fact]
        public void HasValidCorrectAnswer_SingleChoice_MulipleLetters()
        {
            var question = new TestQuestion(1, $"What's the capital city of Germany? {acd}A{ltd}Munich{acd}B{ltd}Berlin", $"A{ald}C", 2, QuestionType.SingleChoice);

            bool valid = questionValidator.HasValidCorrectAnswer(question);

            Assert.False(valid);
        }

        /// <summary>
        /// test HasValidCorrectAnswer on a multiple choice question with valid correct answers
        /// </summary>
        [Fact]
        public void HasValidCorrectAnswer_MultipleChoice_Valid()
        {
            var question = new TestQuestion(2,
                    $"Which of the following cities are located in Czechia? {acd}A{ltd}Brno{acd}B{ltd}Prague{acd}C{ltd}Warsaw",
                    $"B{ald}A", 4, QuestionType.MultipleChoice);

            bool valid = questionValidator.HasValidCorrectAnswer(question);

            Assert.True(valid);
        }

        /// <summary>
        /// test HasValidCorrectAnswer on a multiple choice question with no correct answers -> PASS
        /// </summary>
        [Fact]
        public void HasValidCorrectAnswer_MultipleChoice_NoLetters()
        {
            var question = new TestQuestion(2,
                    $"Which of the following cities are located in Czechia? {acd}A{ltd}Brno{acd}B{ltd}Prague{acd}C{ltd}Warsaw",
                    "", 4, QuestionType.MultipleChoice);

            bool valid = questionValidator.HasValidCorrectAnswer(question);

            Assert.True(valid);
        }

        /// <summary>
        /// test HasValidCorrectAnswer on a multiple choice question with invalid letter in correct answers
        /// </summary>
        [Fact]
        public void HasValidCorrectAnswer_MultipleChoice_InvalidLetter()
        {
            var question = new TestQuestion(2,
                    $"Which of the following cities are located in Czechia? {acd}A{ltd}Brno{acd}B{ltd}Prague{acd}C{ltd}Warsaw",
                    $"B{ald}A{ald}Z", 4, QuestionType.MultipleChoice);

            bool valid = questionValidator.HasValidCorrectAnswer(question);

            Assert.False(valid);
        }
    }
}