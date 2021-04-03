namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing a question in a test
    /// </summary>
    public class TestQuestionVM
    {
        public TestQuestionVM()
        {
        }

        public TestQuestionVM(int number, string text, string correctAnswer, int points) : this()
        {
            Number = number;
            QuestionText = text;
            CorrectAnswer = correctAnswer;
            Points = points;
        }

        /// <summary>
        /// unique identifier of the question
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// number of the question
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// text of the question
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// correct answer to the question
        /// </summary>
        public string CorrectAnswer { get; set; }

        /// <summary>
        /// number of points for this question
        /// </summary>
        public int Points { get; set; }
    }
}