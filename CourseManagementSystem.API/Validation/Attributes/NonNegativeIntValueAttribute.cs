using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.API.Validation.Attributes
{
    /// <summary>
    /// Validation attribute that validates if the integer value is non-negative (e.g. >=0)
    /// <br/>
    /// if validation fails <see cref="defaultErrorMessage"/> is displayed
    /// </summary>
    public class NonNegativeIntValueAttribute : RangeAttribute
    {
        /// <summary>
        /// default error message displayed ({0} is replaced by the field name that this attribute belongs to)
        /// </summary>
        public const string defaultErrorMessage = "The field {0} must be non-negative";

        public NonNegativeIntValueAttribute() : base(0, int.MaxValue)
        {
            ErrorMessage = defaultErrorMessage;
        }
    }
}