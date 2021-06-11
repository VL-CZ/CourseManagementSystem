using CourseManagementSystem.Data.Models;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ICourseService : IDbService
    {
        /// <summary>
        /// get course by its id
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        Course GetById(string courseId);

        /// <summary>
        /// archive course by its id
        /// </summary>
        /// <param name="courseId">id of the course to delete</param>
        void ArchiveById(string courseId);

        /// <summary>
        /// add the course into the database
        /// </summary>
        /// <param name="course">course to add</param>
        void AddCourse(Course course);

        /// <summary>
        /// enroll person to a course
        /// </summary>
        /// <param name="person">person that we enroll to a course</param>
        /// <param name="courseId">id of the course to enroll</param>
        void Enroll(Person person, string courseId);

        /// <summary>
        /// get all files in the course with the given id
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        ICollection<CourseFile> GetFiles(string courseId);

        /// <summary>
        /// get all tests with questions in the course with the given id
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        ICollection<CourseTest> GetTests(string courseId);

        /// <summary>
        /// get all posts posted in this course including their authors
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        ICollection<ForumPost> GetPostsWithAuthors(string courseId);

        /// <summary>
        /// get all members of the course including <see cref="CourseMember.User"/> object
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        ICollection<CourseMember> GetMembersWithUsers(string courseId);

        /// <summary>
        /// get all admins of the course including <see cref="CourseMember.User"/> object
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        ICollection<CourseAdmin> GetAdminsWithUsers(string courseId);
    }
}