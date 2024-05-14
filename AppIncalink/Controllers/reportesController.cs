using Microsoft.AspNetCore.Mvc;
using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Http.Extensions;

using DinkToPdf.Contracts;
using DinkToPdf;
using System.Text;


namespace AppIncalink.Controllers
{
    public class reportesController : Controller
    {
        private readonly IConverter _converter;

        public reportesController(IConverter converter)
        {
            _converter = converter;

        }

        public IActionResult VistaparaPdf()
        {
            return View();

        }
        public IActionResult index()
        {
            return View();
        
        }
        public IActionResult MostrarPdf()
        {

            var listaActividades = new actividadesDatos().listar();

            var htmlTabla = new StringBuilder();
            htmlTabla.Append("<table border='1' cellpadding='10' cellspacing='0'>");
            htmlTabla.Append("<tr><th>Nombre</th><th>Grupo</th><th>Fecha de Inicio</th><th>Fecha Fin</th><th>recursos</th><th>responsables</th><th>lugarDesde</th><th>Observaciones</th><th>tipo de Actividad</th><th>menu</th><th>Vehiculo</th></tr>");

            foreach (var actividad in listaActividades)
            {
                htmlTabla.Append("<tr>");
                htmlTabla.Append($"<td>{actividad.nombre}</td>");
                htmlTabla.Append($"<td>{actividad.nombreGrupo}</td>");
                htmlTabla.Append($"<td>{actividad.fechaInicio}</td>");
                htmlTabla.Append($"<td>{actividad.fechaFin}</td>");
                htmlTabla.Append($"<td>{actividad.recursos}</td>");
                htmlTabla.Append($"<td>{actividad.responsables}</td>");
                htmlTabla.Append($"<td>{actividad.lugarDesde}</td>");
                htmlTabla.Append($"<td>{actividad.LugarHacia}</td>");
                htmlTabla.Append($"<td>{actividad.observaciones}</td>");
                htmlTabla.Append($"<td>{actividad.nombreTipoActividad}</td>");
                htmlTabla.Append($"<td>{actividad.nombreMenu}</td>");
                htmlTabla.Append($"<td>{actividad.nombreVehiculo}</td>");
                htmlTabla.Append("</tr>");
            }

            htmlTabla.Append("</table>");

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
            new ObjectSettings(){
                HtmlContent = htmlTabla.ToString()
            }
        }
            };

            var archivoPDF = _converter.Convert(pdf);

            return File(archivoPDF, "application/pdf");
        }

        public IActionResult DescargarPDF()
        {
            string pagina_actual = HttpContext.Request.Path;
            string url_pagina = HttpContext.Request.GetEncodedUrl();
            url_pagina = url_pagina.Replace(pagina_actual, "");
            url_pagina = $"{url_pagina}/reportes/VistaparaPdf";


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
                    new ObjectSettings(){
                        Page = url_pagina
                    }
                }

            };

            var archivoPDF = _converter.Convert(pdf);
            string nombrePDF = "reporte_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

            return File(archivoPDF, "application/pdf", nombrePDF);
        }



    }
}
