using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.ViewModels
{
    public class PersonVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class StudentVM : PersonVM
    {
        public IEnumerable<GradeDetailsVM> Grades { get; set; }
    }
}
