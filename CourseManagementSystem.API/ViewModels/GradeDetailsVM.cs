using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.ViewModels
{
    public class GradeDetailsVM
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Topic { get; set; }
        public string Comment { get; set; }
    }

    public class AddGradeVM
    {
        public int Value { get; set; }
        public string Topic { get; set; }
        public string Comment { get; set; }
    }
}
