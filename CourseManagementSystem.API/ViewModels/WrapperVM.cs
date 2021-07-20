using CourseManagementSystem.API.Validation.Attributes;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// wrapper viewmodel for simple non-structured types (such as <see cref="int"/>, <see cref="string"/> or <see cref="bool"/>)
    /// </summary>
    /// <typeparam name="T">type of the value</typeparam>
    public class WrapperVM<T>
    {
        public WrapperVM()
        { }

        /// <summary>
        /// create new viewmodel for wrapping non-structured types
        /// </summary>
        /// <param name="value">value of the non-structure type</param>
        public WrapperVM(T value)
        {
            Value = value;
        }

        /// <summary>
        /// value of the object
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public T Value { get; set; }
    }
}