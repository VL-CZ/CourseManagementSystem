using CourseManagementSystem.API.Validation.Attributes;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// base viewmodel for grades
    /// </summary>
    public abstract class BaseGradeVM
    {
        protected BaseGradeVM()
        {
        }

        protected BaseGradeVM(double percentualValue, string topic, string comment, int weight)
        {
            PercentualValue = percentualValue;
            Topic = topic;
            Comment = comment;
            Weight = weight;
        }

        /// <summary>
        /// percentual value of the grade (0=0%, 1=100%, may be greater than 1 in case of bonus points)
        /// </summary>
        [NonNegativeDoubleValue]
        public double PercentualValue { get; set; }

        /// <summary>
        /// topic that this grade belongs to
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Topic { get; set; }

        /// <summary>
        /// comment to the grade provided by teacher
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// weight of the grade - impact on total grade (the higher weight, the more impact)
        /// <br/>
        /// e.g. grade with weight 2 has the same weight as two grades weighted 1
        /// </summary>
        [PositiveIntValue]
        public int Weight { get; set; }
    }

    /// <summary>
    /// viewmodel for adding new grade
    /// </summary>
    public class AddGradeVM : BaseGradeVM
    {
        public AddGradeVM() : base()
        { }
    }

    /// <summary>
    /// class representing grade with its details
    /// </summary>
    public class GradeDetailsVM : BaseGradeVM
    {
        public GradeDetailsVM()
        {
        }

        /// <summary>
        /// create new viewmodel representing details of a grade
        /// </summary>
        /// <param name="id">id of the grade</param>
        /// <param name="percentualValue">value of the grade in double format 0 -> 0%, 1 -> 100%</param>
        /// <param name="topic">topic of the grade</param>
        /// <param name="comment">comment provided to the grade</param>
        /// <param name="weight">weight of the grade</param>
        public GradeDetailsVM(string id, double percentualValue, string topic, string comment, int weight) : base(percentualValue, topic, comment, weight)
        {
            Id = id;
        }

        /// <summary>
        /// identifier of the grade
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Id { get; set; }
    }
}