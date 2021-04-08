using Microsoft.EntityFrameworkCore;
using System;

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
    }
}