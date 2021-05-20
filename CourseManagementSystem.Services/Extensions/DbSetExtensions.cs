using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CourseManagementSystem.Services.Extensions
{
    /// <summary>
    /// class with extension methods for <see cref="DbSet{TEntity}"/>
    /// </summary>
    public static class DbSetExtensions
    {
        /// <summary>
        /// find a item by its id
        /// </summary>
        /// <typeparam name="T">type of items</typeparam>
        /// <param name="dbSet">database set</param>
        /// <param name="id">identifier of the item</param>
        /// <returns></returns>
        public static T FindById<T>(this DbSet<T> dbSet, string id) where T : class
        {
            var guidId = Guid.Parse(id);
            return dbSet.Find(guidId);
        }

        /// <summary>
        /// get id of <see cref="Data.Models.Course"/> that the object belongs to
        /// </summary>
        /// <typeparam name="T">type of items</typeparam>
        /// <param name="dbSet">database set</param>
        /// <param name="objectId">identifier of the object we look for in <paramref name="dbSet"/></param>
        /// <returns></returns>
        public static string GetCourseIdOf<T>(this DbSet<T> dbSet, string objectId) where T : class, ICourseReferenceObject, IGuidIdObject
        {
            return dbSet.Include(item => item.Course)
                .Single(item => item.Id.ToString() == objectId)
                .Course.Id.ToString();
        }

        /// <summary>
        /// get id of <see cref="Data.Models.CourseMember"/> that the object belongs to
        /// </summary>
        /// <typeparam name="T">type of items</typeparam>
        /// <param name="dbSet">database set</param>
        /// <param name="objectId">identifier of the object we look for in <paramref name="dbSet"/></param>
        /// <returns></returns>
        public static string GetCourseMemberIdOf<T>(this DbSet<T> dbSet, string objectId) where T : class, ICourseMemberReferenceObject, IGuidIdObject
        {
            return dbSet.Include(item => item.Student)
                .Single(item => item.Id.ToString() == objectId)
                .Student.Id.ToString();
        }
    }
}