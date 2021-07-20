using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// class representing dictionary of errors
    /// </summary>
    public class ErrorsDictionaryVM
    {
        /// <summary>
        /// errors found
        /// <br/>
        /// in format {errorDescription}:{errorMessage(s)}
        /// </summary>
        public Dictionary<string, string[]> Errors { get; set; }

        public ErrorsDictionaryVM()
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// create new error viewmodel
        /// </summary>
        /// <param name="errors">collection of errors</param>
        public ErrorsDictionaryVM(Dictionary<string, string[]> errors)
        {
            Errors = errors;
        }
    }
}