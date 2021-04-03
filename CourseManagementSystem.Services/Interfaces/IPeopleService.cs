using CourseManagementSystem.Data.Models;

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
    }
}