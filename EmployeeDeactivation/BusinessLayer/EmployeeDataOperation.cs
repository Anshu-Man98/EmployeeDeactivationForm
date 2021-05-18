using EmployeeDeactivation.Data;
using EmployeeDeactivation.Interface;
using EmployeeDeactivation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDeactivation.BusinessLayer
{
    public class EmployeeDataOperation : IEmployeeDataOperation
    {
        private readonly EmployeeDeactivationContext _context;
        public EmployeeDataOperation(EmployeeDeactivationContext context)
        {
            _context = context;
        }

    public string GetReportingManagerEmailId(string teamName)
        {
            var teamDetails = RetrieveAllSponsorDetails();
            foreach (var item in teamDetails)
            {
                if(item.TeamName == teamName)
                {
                    return item.ReportingManagerEmail;
                }
            }
            return "";      
        }

        public  List<DeactivatedEmployeeDetails> SavedEmployeeDetails()
        {
            List<DeactivatedEmployeeDetails> userDetails = new List<DeactivatedEmployeeDetails>();
            var info =  _context.Employees.ToList();
            foreach (var item in info)
            {
                userDetails.Add(new DeactivatedEmployeeDetails
                {
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Email = item.Email,
                    GId = item.GId,
                    Date = item.Date,
                    TeamName = item.TeamName,
                    SponsorName =item.SponsorName,
                    SponsorEmailID =item.SponsorEmailID,
                    Department = item.Department
                });
            }
            return userDetails;
        }

        public List<Teams> RetrieveAllSponsorDetails()
        {
            List<Teams> teamDetails = new List<Teams>();
            var details = _context.Teams.ToList();
            foreach (var item in details)
            {
                teamDetails.Add(new Teams
                {
                    SponsorGID = item.SponsorGID,
                    TeamName = item.TeamName,
                    SponsorFirstName = item.SponsorFirstName,
                    SponsorLastName = item.SponsorLastName,
                    SponsorEmailID = item.SponsorEmailID,
                    Department = item.Department,
                    ReportingManagerEmail = item.ReportingManagerEmail
                });
            }
            return teamDetails;
        }

        public async Task<bool> AddEmployeeData(string firstName, string lastName, string gId, string email, DateTime lastWorkingDate, string teamsName, string sponsorName, string sponsorEmailId, string sponsorDepartment)
        //review change make parameters as class
        {
            DeactivatedEmployeeDetails employee = new DeactivatedEmployeeDetails()
            {
                Firstname = firstName,
                Lastname = lastName,
                GId = gId,
                Email = email,
                Date = lastWorkingDate,
                TeamName = teamsName,
                SponsorName = sponsorName,
                SponsorEmailID = sponsorEmailId,
                Department = sponsorDepartment
            };
            var check = _context.Employees.ToList();
            foreach (var i in check)
            {
                if (i.GId == gId)
                {
                    _context.Remove(_context.Employees.Single(a => a.GId == gId));
                    _context.SaveChanges();
                }
            }

            _context.Add(employee);
            var databaseUpdateStatus = await _context.SaveChangesAsync() == 1 ? true : false;
            return databaseUpdateStatus;
        }

        public DeactivatedEmployeeDetails RetrieveEmployeeDataBasedOnGid(string gId)
        {
            var details = _context.Employees.ToList();
            foreach (var item in details)
            {
                if (item.GId == gId)
                {
                    return item;
                }
            }
            return new DeactivatedEmployeeDetails();

        }
    }

}
