using CourseManagementSystem.Data;
using CourseManagementSystem.Data.Models;
using System.Linq;
using Xunit;

namespace CourseManagementSystem.TestEvaluation.Tests
{
    /// <summary>
    /// tests for methods of <see cref="QuestionWithChoicesTools"/> class
    /// </summary>
    public class QuestionWithChoicesToolsTests
    {
        private const string acd = QuestionWithChoicesTools.answerChoicesDelimiter;
        private const string ltd = QuestionWithChoicesTools.answerChoiceLetterAndTextDelimiter;
        private const string ald = QuestionWithChoicesTools.answerLetterDelimiter;

        private TestQuestion exampleQuestion;
        private TestSubmissionAnswer exampleAnswer;

        public QuestionWithChoicesToolsTests()
        {
            exampleQuestion = new TestQuestion(1, string.Empty, $"A{ald} B", 1, QuestionType.MultipleChoice);
            exampleAnswer = new TestSubmissionAnswer(exampleQuestion, $"A{ald}C");
        }

        /// <summary>
        /// test GetCorrectAnswersLetters method for input with multiple correct answers
        /// </summary>
        [Fact]
        public void GetCorrectAnswersLetters_Normal()
        {
            var correctAnswers = QuestionWithChoicesTools.GetCorrectAnswersLetters(exampleQuestion);

            var expected = new string[] { "A", "B" };
            Assert.True(expected.SequenceEqual(correctAnswers));
        }


        /// <summary>
        /// test GetCorrectAnswersLetters method for input with single correct answer
        /// </summary>
        [Fact]
        public void GetCorrectAnswersLetters_SingleCorrect()
        {
            exampleQuestion.Type = QuestionType.SingleChoice;
            exampleQuestion.CorrectAnswer = "X";

            var correctAnswers = QuestionWithChoicesTools.GetCorrectAnswersLetters(exampleQuestion);

            var expected = new string[] { "X" };
            Assert.True(expected.SequenceEqual(correctAnswers));
        }

        /// <summary>
        /// test GetCorrectAnswersLetters method for input with nonw correct answers
        /// </summary>
        [Fact]
        public void GetCorrectAnswersLetters_NoneCorrect()
        {
            exampleQuestion.CorrectAnswer = "";

            var correctAnswers = QuestionWithChoicesTools.GetCorrectAnswersLetters(exampleQuestion);

            Assert.Empty(correctAnswers);
        }


        /// <summary>
        /// test GetSubmittedAnswersLetters method for input with some selected answers
        /// </summary>
        [Fact]
        public void GetSubmittedAnswersLetters_Normal()
        {
            var submittedLetters = QuestionWithChoicesTools.GetSubmittedAnswersLetters(exampleAnswer);

            var expected = new string[] { "A", "C" };
            Assert.True(expected.SequenceEqual(submittedLetters));
        }

        /// <summary>
        /// test GetSubmittedAnswersLetters method for input with no selected answers
        /// </summary>
        [Fact]
        public void GetSubmittedAnswersLetters_Empty()
        {
            exampleAnswer.Text = "";
            var submittedLetters = QuestionWithChoicesTools.GetSubmittedAnswersLetters(exampleAnswer);

            Assert.Empty(submittedLetters);
        }

        /// <summary>
        /// test GetAnswerChoicesLetters method for input with several answer choices
        /// </summary>
        [Fact]
        public void GetAnswerChoicesLetters_Normal()
        {
            exampleQuestion.QuestionText = $"What is this?{acd}A{ltd}some text{acd}B{ltd}some text{acd}C{ltd}some text";

            var choicesLetters = QuestionWithChoicesTools.GetAnswerChoicesLetters(exampleQuestion);

            var expected = new string[] { "A", "B", "C" };
            Assert.True(expected.SequenceEqual(choicesLetters));
        }

        /// <summary>
        /// test GetAnswerChoicesLetters method for input with no answer choices
        /// </summary>
        [Fact]
        public void GetAnswerChoicesLetters_None()
        {
            exampleQuestion.QuestionText = $"What is this?";

            var choicesLetters = QuestionWithChoicesTools.GetAnswerChoicesLetters(exampleQuestion);

            Assert.Empty(choicesLetters);
        }
    }
}