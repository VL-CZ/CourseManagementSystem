using CourseManagementSystem.Data.Models;

namespace CourseManagementSystem.API.Services
{
    public interface IPersonService
    {
        Person GetPersonByID(string id);

        void RemovePersonById(string id);
    }
}