using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeDeactivation.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using EmployeeDeactivation.BusinessLayer;
using EmployeeDeactivation.Interface;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;

namespace EmployeeDeactivation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeDataOperations _employeeDataOperation;

        public EmployeesController(IEmployeeDataOperations employeeDataOperation)
        {
            _employeeDataOperation = employeeDataOperation;
        }

        [HttpGet]
        public IActionResult Pdf()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpGet]
        [Route("Employees/GetSponsorDetails")]
        public JsonResult GetSponsorDetails()
        {
            return Json(_employeeDataOperation.RetrieveEmployeeData());    
        }


        [HttpPost]
        public async Task<JsonResult> AddDetails(string firstName, string lastName, string gId, string email, DateTime lastWorkingDate)
        {
            var updateStatus = await _employeeDataOperation.AddEmployeeData(firstName, lastName, gId, email, lastWorkingDate);
            //if(updateStatus==true)
            //{
            //    ViewBag.Message = "Deactivation Form has been Submitted Successfully";
            //}
            //else
            //{
            //    ViewBag.Message = "Deactivation Form Not Submitted Successfully";
            //}

               return Json("");

            
        }

    }
}
