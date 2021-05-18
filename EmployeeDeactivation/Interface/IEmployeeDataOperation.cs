using EmployeeDeactivation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeDeactivation.Interface
{
    public interface IEmployeeDataOperation
    {
        List<Teams> RetrieveAllSponsorDetails();
        Task<bool> AddEmployeeData(string firstName, string lastName, string gId, string email, DateTime lastWorkingDate, string teamName, string sponsorName, string sponsorEmailId, string sponsorDepartment);
        DeactivatedEmployeeDetails RetrieveEmployeeDataBasedOnGid(string gId);
        string GetReportingManagerEmailId(string teamName);
        List<DeactivatedEmployeeDetails> SavedEmployeeDetails();

    }
}
