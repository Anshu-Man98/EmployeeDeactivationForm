using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDeactivation.Interface;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf;
using System.Drawing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System.Net.Mail;
using EmployeeDeactivation.Models;
using System.Net;

namespace EmployeeDeactivation.Controllers
{

    public class PdfController : Controller
    {
        private readonly IEmployeeDataOperations _employeeDataOperation;

        public PdfController(IEmployeeDataOperations employeeDataOperation)
        {
            _employeeDataOperation = employeeDataOperation;
        }
        [HttpGet]
        [Route("Pdf/Index")]
        public IActionResult Index(string GId)
        {
            var employeeData = _employeeDataOperation.RetrieveEmployeeDataBasedOnGid(GId);
            //Load the PDF document
            FileStream docStream = new FileStream("DeactivationForm.pdf", FileMode.Open, FileAccess.Read);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            //Loads the form
            PdfLoadedForm form = loadedDocument.Form;

            //Fills the textbox field by using index
            (form.Fields[0] as PdfLoadedTextBoxField).Text = employeeData.Firstname;
            (form.Fields[1] as PdfLoadedTextBoxField).Text = employeeData.Lastname;
            (form.Fields[2] as PdfLoadedTextBoxField).Text = employeeData.Email;
            (form.Fields[3] as PdfLoadedTextBoxField).Text = employeeData.GId;
            (form.Fields[4] as PdfLoadedTextBoxField).Text = employeeData.Date.ToString();
            (form.Fields[5] as PdfLoadedTextBoxField).Text = employeeData.TeamName;
            (form.Fields[6] as PdfLoadedTextBoxField).Text = employeeData.SponsorName;
            (form.Fields[7] as PdfLoadedTextBoxField).Text = employeeData.SponsorEmailID;
            (form.Fields[8] as PdfLoadedTextBoxField).Text = employeeData.Department;

            MemoryStream stream = new MemoryStream();


            loadedDocument.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;

            //Close the document.
            loadedDocument.Close(true);

            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";

            //Define the file name.
            string fileName = "output.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.

            byte[] bytes = stream.ToArray();

            return Json("data:application/pdf;base64," + Convert.ToBase64String(bytes));

            //return File(stream, contentType, fileName);
            //return Json((File(stream, contentType, fileName)), new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpPost]
        [Route("Pdf/PdfAttachment")]
        public void PdfAttachment(string memoryStream)
        {
            byte[] bytes = System.Convert.FromBase64String(memoryStream);
            var c = bytes;
            MemoryStream stream = new MemoryStream(bytes);
            //Attach the file

            Attachment file = new Attachment(stream, "Attachment.pdf", "application/pdf");
            MailMessage message = new MailMessage();
            // end-user customization
            message.From = new MailAddress("jjffrr453@gmail.com");
            message.Sender = new MailAddress("jjffrr453@gmail.com");
            message.To.Add("sonalisingh7639@gmail.com");
            message.Subject = "message";
            message.Attachments.Add(file);
            message.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("jjffrr453@gmail.com", "123star123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }

    }
}
