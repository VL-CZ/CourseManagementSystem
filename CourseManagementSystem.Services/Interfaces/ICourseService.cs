using CourseManagementSystem.Data.Models;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Interfaces
{
    public interface ICourseService
    {
        /// <summary>
        /// get course by its id
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        Course GetById(int courseId);

        /// <summary>
        /// delete course by its id
        /// </summary>
        /// <param name="courseId">id of the course to delete</param>
        void DeleteById(int courseId);

        /// <summary>
        /// add the course into the database
        /// </summary>
        /// <param name="course">course to add</param>
        void AddCourse(Course course);

        /// <summary>
        /// get all files in the course with the given id
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        ICollection<CourseFile> GetFiles(int courseId);

        /// <summary>
        /// get all tests in the course with the given id
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        ICollection<CourseTest> GetTests(int courseId);

        /// <summary>
        /// get all posts posted in this course including their authors
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        ICollection<ForumPost> GetPostsWithAuthors(int courseId);

        /// <summary>
        /// get all members of the course
        /// </summary>
        /// <param name="courseId">identifier of the course</param>
        /// <returns></returns>
        ICollection<CourseMember> GetMembers(int courseId);
    }
}