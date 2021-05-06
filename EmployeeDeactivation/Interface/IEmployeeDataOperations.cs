using EmployeeDeactivation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDeactivation.Interface
{
    public interface IEmployeeDataOperations
    {
        List<Teams> RetrieveEmployeeData();
        Task<bool> AddEmployeeData(string firstName, string lastName, string gId, string email, DateTime lastWorkingDate);
    }

}
