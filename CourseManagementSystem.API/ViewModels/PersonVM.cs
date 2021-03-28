using System.Collections.Generic;

namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing user of the app
    /// </summary>
    public class PersonVM
    {
        /// <summary>
        /// identifier of the person
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// name of the person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// email of the person
        /// </summary>
        public string Email { get; set; }
    }

    public class StudentVM : PersonVM
    {
        public IEnumerable<GradeDetailsVM> Grades { get; set; }
    }

    public class PersonIdVM
    {
        public string Id { get; set; }
    }

    public class IsAdminVM
    {
        public bool IsAdmin { get; set; }
    }
}