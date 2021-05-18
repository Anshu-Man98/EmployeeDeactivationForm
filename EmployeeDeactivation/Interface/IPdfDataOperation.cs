using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDeactivation.Interface
{
    public interface IPdfDataOperation
    {
        void SendPdfAsEmailAttachment(string memoryStream, string employeeName, string teamName);
        byte[] FillPdfForm(string GId);
        void SendReminderEmail();
        
    }
}
