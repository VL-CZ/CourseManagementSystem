﻿using CourseManagementSystem.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.API.Services
{
    public interface IPersonService
    {
        Person GetPersonByID(string id);
        void RemovePersonById(string id);
    }
}
