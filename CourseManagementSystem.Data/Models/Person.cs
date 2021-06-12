using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CourseManagementSystem.Data.Models
{
    /// <summary>
    /// class representing user of the application
    /// </summary>
    public class Person : IdentityUser
    {
        public Person() : base()
        {
            CourseMemberships = new List<CourseMember>();
            AdminMemberships = new List<CourseAdmin>();
        }

        /// <summary>
        /// course memberships of the user
        /// </summary>
        public ICollection<CourseMember> CourseMemberships { get; set; }

        /// <summary>
        /// admin memberships the user
        /// </summary>
        public ICollection<CourseAdmin> AdminMemberships { get; set; }
    }
}