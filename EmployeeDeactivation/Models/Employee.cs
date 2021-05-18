using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EmployeeDeactivation.Controllers;
using System.IO;
using Timer = System.Timers.Timer;
using System.Timers;
using EmployeeDeactivation.Interface;

namespace EmployeeDeactivation.Models
{
    public class Employee
    {
        private readonly IPdfDataOperation _pdfDataOperation;
        public Employee(IPdfDataOperation pdfDataOperation)
        {
            _pdfDataOperation = pdfDataOperation;
        }
        private void OnTimedEventAsync(object sender, ElapsedEventArgs e)
        {
            _pdfDataOperation.SendRemainderEmail();

        }
        public void SetTimer()
        {​​​​​
            var aTimer = new Timer(600000);
            aTimer.Elapsed += OnTimedEventAsync;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }​​​​​
    }
}
