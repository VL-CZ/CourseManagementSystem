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

        /// <summary>
        /// content of the file
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// name of the file
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// content type of the data
        /// </summary>
        public string ContentType { get; set; }
    }
}
