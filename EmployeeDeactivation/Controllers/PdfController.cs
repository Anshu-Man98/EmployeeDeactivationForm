using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf.Parsing;

namespace EmployeeDeactivation.Controllers
{
    public class PdfController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //Load the PDF document
            FileStream docStream = new FileStream("nn.pdf", FileMode.Open, FileAccess.Read);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            //Loads the form
            PdfLoadedForm form = loadedDocument.Form;
            //Fills the textbox field by using index
            (form.Fields[0] as PdfLoadedTextBoxField).Text = "John";
            //Fills the textbox fields by using field name
            //(form.Fields["Firstname"] as PdfLoadedTextBoxField).Text = "Doe";
            //(form.Fields["Address"] as PdfLoadedTextBoxField).Text = " John Doe \n 123 Main St \n Anytown, USA";
            ////Loads the radio button group
            //PdfLoadedRadioButtonItemCollection radioButtonCollection = (form.Fields["Gender"] as PdfLoadedRadioButtonListField).Items;
            ////Checks the 'Male' option
            //radioButtonCollection[0].Checked = true;
            ////Checks the 'business' checkbox field
            //(form.Fields["Business"] as PdfLoadedCheckBoxField).Checked = true;
            ////Checks the 'retiree' checkbox field
            //(form.Fields["Retiree"] as PdfLoadedCheckBoxField).Checked = true;
            //Save the PDF document to stream
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
            return File(stream, contentType, fileName);
        }
    }
}