using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// class representing evaluated test submission
    /// </summary>
    public class EvaluatedTestSubmissionVM
    {
        public EvaluatedTestSubmissionVM()
        {
            EvaluatedAnswers = new List<EvaluatedAnswerVM>();
        }

        public EvaluatedTestSubmissionVM(string testSubmissionId, ICollection<EvaluatedAnswerVM> evaluatedAnswers)
        {
            TestSubmissionId = testSubmissionId;
            EvaluatedAnswers = evaluatedAnswers;
        }

        /// <summary>
        /// id of the test submission these properties belongs to
        /// </summary>
        public string TestSubmissionId { get; set; }

        /// <summary>
        /// collection of evaluated answers
        /// </summary>
        public ICollection<EvaluatedAnswerVM> EvaluatedAnswers { get; set; }
    }

    /// <summary>
    /// class representing a single evaluated answer of the solution
    /// </summary>
    public class EvaluatedAnswerVM
    {
        public EvaluatedAnswerVM()
        { }

        public EvaluatedAnswerVM(int questionNumber, int updatedPoints, string updatedComment)
        {
            QuestionNumber = questionNumber;
            UpdatedPoints = updatedPoints;
            UpdatedComment = updatedComment;
        }

        /// <summary>
        /// number of test question that these data belong to
        /// </summary>
        public int QuestionNumber { get; set; }

        /// <summary>
        /// updated points for the answer
        /// </summary>
        public int UpdatedPoints { get; set; }

        /// <summary>
        /// updated comment for the answer
        /// </summary>
        public string UpdatedComment { get; set; }
    }
}