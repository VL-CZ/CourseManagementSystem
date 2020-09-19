using CourseManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.Services
{
    public interface IPersonService
    {
        Person GetPersonByID(int id);
        void RemovePersonById(int personId);
    }
}
