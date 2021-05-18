using EmployeeDeactivation.Interface;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmployeeDeactivation.BusinessLayer
{

    public class PdfDataOperation : IPdfDataOperation
    {
        private readonly IEmployeeDataOperation _employeeDataOperation;

        public PdfDataOperation(IEmployeeDataOperation employeeDataOperation)
        {
            _employeeDataOperation = employeeDataOperation;
        }

        public byte[] FillPdfForm(string GId)
        {
            var employeeData = _employeeDataOperation.RetrieveEmployeeDataBasedOnGid(GId);
            FileStream docStream = new FileStream("DeactivateForm.pdf", FileMode.Open, FileAccess.Read);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            PdfLoadedForm form = loadedDocument.Form;
            (form.Fields[9] as PdfLoadedTextBoxField).Text = employeeData.Firstname;
            (form.Fields[8] as PdfLoadedTextBoxField).Text = employeeData.Lastname;
            (form.Fields[11] as PdfLoadedTextBoxField).Text = employeeData.Email;
            (form.Fields[10] as PdfLoadedTextBoxField).Text = employeeData.GId;
            (form.Fields[4] as PdfLoadedTextBoxField).Text = employeeData.Date.ToString();
            (form.Fields[14] as PdfLoadedTextBoxField).Text = employeeData.TeamName;
            (form.Fields[15] as PdfLoadedTextBoxField).Text = employeeData.SponsorName;
            (form.Fields[16] as PdfLoadedTextBoxField).Text = employeeData.SponsorEmailID;
            (form.Fields[17] as PdfLoadedTextBoxField).Text = employeeData.Department;
            MemoryStream stream = new MemoryStream();
            loadedDocument.Save(stream);
            stream.Position = 0;
            loadedDocument.Close(true);
            byte[] bytes = stream.ToArray();
            return bytes;
        }

        public void SendPdfAsEmailAttachment(string memoryStream, string employeeName, string teamName)
        {
            var reportingManagerEmailId = _employeeDataOperation.GetReportingManagerEmailId(teamName);
            byte[] bytes = System.Convert.FromBase64String(memoryStream);
            var c = bytes;
            MemoryStream stream = new MemoryStream(bytes);
            Attachment file = new Attachment(stream, "Deactivation workflow_" + employeeName + ".pdf", "application/pdf");
            MailMessage message = new MailMessage();
            message.From = new MailAddress("jjffrr453@gmail.com");
            message.Sender = new MailAddress("jjffrr453@gmail.com");
            message.To.Add(reportingManagerEmailId);
            message.Subject = "Deactivation workflow initiated";
            message.Attachments.Add(file);
            message.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("jjffrr453@gmail.com", "123star123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        public void SendRemainderEmail()
        {


        }
    }
}
