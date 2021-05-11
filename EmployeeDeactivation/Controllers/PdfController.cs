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
    }
}
