using CourseManagementSystem.API.Validation.Attributes;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// wrapper viewmodel for simple types (such as <see cref="int"/>, <see cref="string"/> or <see cref="bool"/>)
    /// </summary>
    /// <typeparam name="T">type of the value</typeparam>
    public class WrapperVM<T>
    {
        public WrapperVM()
        { }

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