using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EmployeeDeactivation.Controllers;

namespace EmployeeDeactivation.Models
{
    public class Employee
    {
       
        public string Firstname { get; set; }

       
        public string Lastname { get; set; }

       
        public string Email { get; set; }

        [Key]
        public string GId { get; set; }

      
        public DateTime Date { get; set; }

       

    }
   

}
