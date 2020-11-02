using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing user of the application
    /// </summary>
    public class Person : IdentityUser
    {
        /// <summary>
        /// grades of this student
        /// </summary>
        public ICollection<CourseMembership> CourseMemberships { get; set; }

    }
}
