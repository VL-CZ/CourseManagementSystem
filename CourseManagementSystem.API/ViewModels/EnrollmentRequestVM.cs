using CourseManagementSystem.API.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.ViewModels
{
    public class EnrollmentRequestVM
    {
        public EnrollmentRequestVM()
        {
        }

        public EnrollmentRequestVM(string id, string personName, string personEmail)
        {
            Id = id;
            PersonName = personName;
            PersonEmail = personEmail;
        }

        /// <summary>
        /// identifier of enrollment request
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string Id { get; set; }

        /// <summary>
        /// name of the person that requested the enrollment
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string PersonName { get; set; }

        /// <summary>
        /// email of the person that requested the enrollment
        /// </summary>
        [RequiredWithDefaultErrorMessage]
        public string PersonEmail { get; set; }
    }
}
