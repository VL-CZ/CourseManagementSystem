using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.API.Validation.Attributes
{
    /// <summary>
    /// Validation attribute that validates if the double value is non-negative (e.g. >=0)
    /// <br/>
    /// if validation fails <see cref="defaultErrorMessage"/> is displayed
    /// </summary>
    public class NonNegativeDoubleValueAttribute : RangeAttribute
    {
        /// <summary>
        /// default error message displayed ({0} is replaced by the field name that this attribute belongs to)
        /// </summary>
        public const string defaultErrorMessage = "The field {0} must be non-negative";

        public NonNegativeDoubleValueAttribute() : base(0, double.MaxValue)
        {
            ErrorMessage = defaultErrorMessage;
        }
    }
}