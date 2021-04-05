using CourseManagementSystem.Data.Models;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface IPeopleService
    {
        /// <summary>
        /// get person by its id
        /// </summary>
        /// <param name="personId">identifier of the person</param>
        /// <returns></returns>
        Person GetById(string personId);

        /// <summary>
        /// enroll person to a course
        /// </summary>
        /// <param name="person">person that we enroll to a course</param>
        /// <param name="course">course where to enroll</param>
        void EnrollTo(Person person, Course course);

        /// <summary>
        /// get managed courses of the person (e.g. all courses where the person is an admin)
        /// </summary>
        /// <param name="personId">id of the person</param>
        /// <returns></returns>
        IEnumerable<Course> GetManagedCourses(string personId);

        /// <summary>
        /// get all courses whose member the given person is
        /// </summary>
        /// <param name="personId">id of the person</param>
        /// <returns></returns>
        IEnumerable<Course> GetMemberCourses(string personId);

        /// <summary>
        /// get course membership of the <paramref name="person"/> in <paramref name="course"/>
        /// <br/>
        /// or return null if the <paramref name="person"/> is not member of <paramref name="course"/>
        /// </summary>
        /// <param name="person">selected person</param>
        /// <param name="course">selected course</param>
        /// <returns></returns>
        CourseMember GetCourseMembership(Person person, Course course);
    }
}