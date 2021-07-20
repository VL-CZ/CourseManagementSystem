using CourseManagementSystem.API.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing a request for enrollment
    /// </summary>
    public class EnrollmentRequestVM
    {
        public EnrollmentRequestVM()
        {
        }

        /// <summary>
        /// create new instance of viewmodel representing enrollment requests
        /// </summary>
        /// <param name="id">id of the enrollment request</param>
        /// <param name="personName">name of the person that requested the enrollment</param>
        /// <param name="personEmail">email of the person that requested the enrollment</param>
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
