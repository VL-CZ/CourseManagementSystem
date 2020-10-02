using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.API.Services
{
    public interface IPersonService
    {
        /// <summary>
        /// get person with selected person ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Person GetPersonByID(string id);

        /// <summary>
        /// remove person with selected ID
        /// </summary>
        /// <param name="id"></param>
        void RemovePersonById(string id);
    }
}