using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseManagementSystem.Data.Models
{
    public class CourseFile
    {
        [Key]
        public int ID { get; set; }

        public FormFile File { get; set; }
    }
}
