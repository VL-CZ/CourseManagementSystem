﻿using CourseManagementSystem.API.Validation.Attributes;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing a shared file in the course
    /// </summary>
    public class CourseFileVM
    {
        public CourseFileVM()
        {
        }

        public CourseFileVM(string id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// identifier of the file
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Id { get; set; }

        /// <summary>
        /// name of the file
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Name { get; set; }
    }
}