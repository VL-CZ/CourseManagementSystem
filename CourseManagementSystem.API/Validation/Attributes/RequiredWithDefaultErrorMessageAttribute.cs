using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.API.Validation.Attributes
{
    /// <summary>
    /// Validation attribute that marks this value as required
    /// <br/>
    /// if validation fails <see cref="defaultErrorMessage"/> is displayed
    /// </summary>
    public class RequiredWithDefaultErrorMessageAttribute : RequiredAttribute
    {
        /// <summary>
        /// default error message displayed ({0} is replaced by the field name that this attribute belongs to)
        /// </summary>
        public const string defaultErrorMessage = "The field {0} is required";

        public RequiredWithDefaultErrorMessageAttribute()
        {
            ErrorMessage = defaultErrorMessage;
        }
    }
}