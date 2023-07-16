using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace NamrataKalyani.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetPDF()
        {
            var model = new InvoiceModel();
            model.InvoiceNumber = 1211;
            model.CustomerName = "Test Ali";
            model.TotalAmount = 1000;
            return RedirectToAction("GeneratePDF",model);
        }
        public ActionResult GeneratePDF(InvoiceModel model)
        {
           
            // Render the view to a string
            string viewHtml = RenderViewToString("InvoicePreview", model);

            // Generate the PDF from the view HTML
            byte[] pdfBytes;
            using (var ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                using (var sr = new StringReader(viewHtml))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                }
                document.Close();
                pdfBytes = ms.ToArray();
            }

            // Return the PDF file as a downloadable content
            return File(pdfBytes, "application/pdf");
        }

        private string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.ToString();
            }
        }
    }

    public class InvoiceModel
    {
        public int InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public int TotalAmount { get; set; }
    }
}