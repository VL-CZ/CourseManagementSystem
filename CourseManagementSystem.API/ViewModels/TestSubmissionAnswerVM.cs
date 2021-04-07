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

        public SubmissionAnswerVM(int questionNumber, string questionText, string answerText)
        {
            QuestionNumber = questionNumber;
            QuestionText = questionText;
            AnswerText = answerText;
        }

        /// <summary>
        /// number of question that this answer belongs to
        /// </summary>
        public int QuestionNumber { get; set; }

        /// <summary>
        /// text of the question
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// answer submitted by the student
        /// </summary>
        public string AnswerText { get; set; }
    }

    /// <summary>
    /// this class contains student's answer and the correct answer
    /// </summary>
    public class SubmissionAnswerWithCorrectAnswerVM : SubmissionAnswerVM
    {
        public SubmissionAnswerWithCorrectAnswerVM(int questionNumber, string questionText, string answerText, string correctAnswer, int receivedPoints, int maximalPoints, string comment)
            : base(questionNumber, questionText, answerText)
        {
            CorrectAnswer = correctAnswer;
            ReceivedPoints = receivedPoints;
            MaximalPoints = maximalPoints;
            Comment = comment;
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

        /// <summary>
        /// comment to the answer provided by teacher
        /// </summary>
        public string Comment { get; }
    }
}