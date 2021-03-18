using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// this class represents viewmodel for test with submission
    /// </summary>
    public class TestWithSubmissionVM
    {
        public TestWithSubmissionVM(int testId, string testTopic, int submissionId, IEnumerable<SubmissionAnswerWithCorrectAnswerVM> answers)
        {
            TestId = testId;
            TestTopic = testTopic;
            Answers = answers;
            SubmissionId = submissionId;
        }

        /// <summary>
        /// id of the test
        /// </summary>
        public int TestId { get; }

        /// <summary>
        /// topic of the test
        /// </summary>
        public string TestTopic { get; }

        /// <summary>
        /// id of the test submission
        /// </summary>
        public int SubmissionId { get; }

        /// <summary>
        /// submitted and correct answers
        /// </summary>
        public IEnumerable<SubmissionAnswerWithCorrectAnswerVM> Answers { get; }
    }

    /// <summary>
    /// this class contains student's answer and the correct answer
    /// </summary>
    public class SubmissionAnswerWithCorrectAnswerVM : SubmissionAnswerVM
    {
        public SubmissionAnswerWithCorrectAnswerVM(int questionNumber, string questionText, string answerText, string correctAnswer, int receivedPoints, int maximalPoints)
            : base(questionNumber, questionText, answerText)
        {
            CorrectAnswer = correctAnswer;
            ReceivedPoints = receivedPoints;
            MaximalPoints = maximalPoints;
        }

        /// <summary>
        /// text of the correct answer
        /// </summary>
        public string CorrectAnswer { get; }

        /// <summary>
        /// points received for the answer
        /// </summary>
        public int ReceivedPoints { get; }

        /// <summary>
        /// maximal obtained points for the question
        /// </summary>
        public int MaximalPoints { get; }
    }
}
