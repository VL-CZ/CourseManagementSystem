﻿using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.API.Validation.Attributes
{
    /// <summary>
    /// Validation attribute that validates if the integer value is >= 1
    /// <br/>
    /// if validation fails <see cref="defaultErrorMessage"/> is displayed
    /// </summary>
    public class PositiveIntValueAttribute : RangeAttribute
    {
        /// <summary>
        /// default error message displayed ({0} is replaced by the field name that this attribute belongs to)
        /// </summary>
        public const string defaultErrorMessage = "The field {0} must be a positive integer (greater than or equal to 1)";

        /// <summary>
        /// create new instance of validation attribute that validates if the integer value is >= 1
        /// </summary>
        public PositiveIntValueAttribute() : base(1, int.MaxValue)
        {
            ErrorMessage = defaultErrorMessage;
        }
    }
}