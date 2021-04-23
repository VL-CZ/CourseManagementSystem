using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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
        public ICollection<CourseMember> CourseMemberships { get; set; } = new List<CourseMember>();
    }
}