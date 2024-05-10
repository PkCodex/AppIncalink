using Microsoft.AspNetCore.Mvc;
using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;
using System.Data.SqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
namespace AppIncalink.Controllers
{
    public class reportesController : Controller
    {
        
        public IActionResult Reportes()
        {
            return View();
        }

        public IActionResult GenerarPDF()
        {
            // Creamos un nuevo documento PDF
            PdfDocument document = new PdfDocument();

            // Agregamos una página al documento
            PdfPage page = document.AddPage();

            // Obtenemos un objeto XGraphics para dibujar en la página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Creamos una fuente y un formato de texto
            XFont font = new XFont("Fuentes\\Roboto-Bold.ttf", 20);

            XStringFormat format = new XStringFormat();

            // Dibujamos un texto en la página
            gfx.DrawString("¡Hola, mundo!", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center);

            // Guardamos el documento en un array de bytes
            byte[] pdfBytes;
            using (var stream = new System.IO.MemoryStream())
            {
                document.Save(stream, false);
                pdfBytes = stream.ToArray();
            }

            // Retornamos el archivo PDF como un archivo descargable
            return File(pdfBytes, "application/pdf", "ejemplo.pdf");
        }
    }
}
