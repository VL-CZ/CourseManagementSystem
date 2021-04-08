using System;

namespace CourseManagementSystem.Data
{
    /// <summary>
    /// interface for object with <see cref="Guid"/> identifier
    /// </summary>
    public interface IGuidIdObject
    {
        /// <summary>
        /// identifier of the object
        /// </summary>
        Guid Id { get; }
    }
}
