using CourseManagementSystem.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Services.Extensions
{
    /// <summary>
    /// class with extension methods for <see cref="IEnumerable{Course}"/>
    /// </summary>
    public static class IEnumerableCourseExtensions
    {
        /// <summary>
        /// filter active courses
        /// </summary>
        /// <param name="courses">enumerable of courses</param>
        /// <returns>all active courses in <paramref name="courses"/></returns>
        public static IEnumerable<Course> FilterActive(this IEnumerable<Course> courses)
        {
            return courses.Where(course => !course.IsArchived);
        }
    }
}