namespace CourseManagementSystem.API.ViewModels
{
    /// <summary>
    /// viewmodel representing member of a course
    /// </summary>
    public class CourseMemberVM
    {
        public CourseMemberVM(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

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

    public class PersonIdVM
    {
        public string Id { get; set; }
    }

    public class IsAdminVM
    {
        public bool IsAdmin { get; set; }
    }
}