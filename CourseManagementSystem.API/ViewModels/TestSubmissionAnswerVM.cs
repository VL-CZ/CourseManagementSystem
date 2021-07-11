using CourseManagementSystem.API.Validation.Attributes;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// class representing the submission answers submitted by students
    /// </summary>
    public class SubmissionAnswerVM
    {
        public SubmissionAnswerVM()
        {
        }

        public SubmissionAnswerVM(int questionNumber, string questionText, string answerText, Data.QuestionType questionType)
        {
            QuestionNumber = questionNumber;
            QuestionText = questionText;
            AnswerText = answerText;
            QuestionType = questionType;
        }

        /// <summary>
        /// number of question that this answer belongs to
        /// </summary>
        [PositiveIntValue]
        public int QuestionNumber { get; set; }

        /// <summary>
        /// text of the question
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string QuestionText { get; set; }

        /// <summary>
        /// answer submitted by the student
        /// </summary>
        public string AnswerText { get; set; }

        /// <summary>
        /// type of the question
        /// </summary>
        public Data.QuestionType QuestionType { get; set; }
    }

    /// <summary>
    /// this class contains student's answer and the correct answer
    /// </summary>
    public class SubmissionAnswerWithCorrectAnswerVM : SubmissionAnswerVM
    {
        public SubmissionAnswerWithCorrectAnswerVM() : base()
        { }

        public SubmissionAnswerWithCorrectAnswerVM(int questionNumber, string questionText, string answerText,
            string correctAnswer, int receivedPoints, int maximalPoints, string comment, Data.QuestionType questionType)
            : base(questionNumber, questionText, answerText, questionType)
        {
            CorrectAnswer = correctAnswer;
            ReceivedPoints = receivedPoints;
            MaximalPoints = maximalPoints;
            Comment = comment;
        }

        /// <summary>
        /// text of the correct answer
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string CorrectAnswer { get; set; }

        /// <summary>
        /// points received for the answer
        /// </summary>
        [NonNegativeIntValue]
        public int ReceivedPoints { get; set; }

        /// <summary>
        /// maximal obtained points for the question
        /// </summary>
        [PositiveIntValue]
        public int MaximalPoints { get; set; }

        /// <summary>
        /// comment to the answer provided by teacher
        /// </summary>
        public string Comment { get; set; }
    }
}