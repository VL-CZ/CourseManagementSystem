using CourseManagementSystem.API.Validation.Attributes;
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

        /// <summary>
        /// create new viewmodel representing evaluated test submission
        /// </summary>
        /// <param name="testSubmissionId">identifier of the test submission</param>
        /// <param name="evaluatedAnswers">collection of evaluated answers</param>
        public EvaluatedTestSubmissionVM(string testSubmissionId, ICollection<EvaluatedAnswerVM> evaluatedAnswers)
        {
            TestSubmissionId = testSubmissionId;
            EvaluatedAnswers = evaluatedAnswers;
        }

        /// <summary>
        /// id of the test submission these properties belongs to
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string TestSubmissionId { get; set; }

        /// <summary>
        /// collection of evaluated answers
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public ICollection<EvaluatedAnswerVM> EvaluatedAnswers { get; set; }
    }

    /// <summary>
    /// class representing a single evaluated answer of the solution
    /// </summary>
    public class EvaluatedAnswerVM
    {
        public EvaluatedAnswerVM()
        { }

        /// <summary>
        /// create new instance of viewmodel representing a single evaluated answer of the solution
        /// </summary>
        /// <param name="questionNumber">number of the question</param>
        /// <param name="updatedPoints">updated number of points</param>
        /// <param name="updatedComment">updated comment to the question</param>
        public EvaluatedAnswerVM(int questionNumber, int updatedPoints, string updatedComment)
        {
            QuestionNumber = questionNumber;
            UpdatedPoints = updatedPoints;
            UpdatedComment = updatedComment;
        }

        /// <summary>
        /// number of test question that these data belong to
        /// </summary>
        [PositiveIntValue]
        public int QuestionNumber { get; set; }

        /// <summary>
        /// updated points for the answer
        /// </summary>
        [NonNegativeIntValue]
        public int UpdatedPoints { get; set; }

        /// <summary>
        /// updated comment for the answer
        /// </summary>
        public string UpdatedComment { get; set; }
    }
}