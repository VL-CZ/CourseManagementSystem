using CourseManagementSystem.Data.Models;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface IPeopleService : IDbService
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
        /// add admin to a course
        /// </summary>
        /// <param name="admin">person that we add as an admin</param>
        /// <param name="course">course where to add</param>
        void AddAdmin(Person admin, Course course);

        /// <summary>
        /// get all active courses managed by the person (e.g. all courses where the person is an admin)
        /// </summary>
        /// <param name="personId">id of the person</param>
        /// <returns></returns>
        IEnumerable<Course> GetActiveManagedCourses(string personId);

        /// <summary>
        /// get all active courses whose member the given person is
        /// </summary>
        /// <param name="personId">id of the person</param>
        /// <returns></returns>
        IEnumerable<Course> GetActiveMemberCourses(string personId);

        /// <summary>
        /// check if the given person is admin of the course
        /// </summary>
        /// <param name="personId">identifier of the person</param>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        bool IsAdminOfCourse(string personId, string courseId);

        /// <summary>
        /// check if the given person is member of the course
        /// </summary>
        /// <param name="personId">identifier of the person</param>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        bool IsMemberOfCourse(string personId, string courseId);

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