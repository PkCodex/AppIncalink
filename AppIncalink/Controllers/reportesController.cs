using Microsoft.AspNetCore.Mvc;
using DinkToPdf;
using DinkToPdf.Contracts;
using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;
using System.Data.SqlClient;
namespace AppIncalink.Controllers
{
    public class reportesController : Controller
    {
        private readonly IConverter _converter;

        public reportesController(IConverter converter)
        {
            _converter = converter;
        }
        public IActionResult Reportes()
        {
            return View();
        }
        

        public IActionResult MostrarPDFGrupo(int idGrupo)
        {
            var listaPersonas = new grupoDatos().ObtenerPersonasPorGrupo(idGrupo);

            var htmlTabla = new StringBuilder();
            htmlTabla.Append("<table border='1' cellpadding='10' cellspacing='0'>");
            htmlTabla.Append("<tr><th>Nombre Completo</th><th>Sexo</th><th>Documento ID</th><th>Correo</th><th>Telefono</th><th>Alergia Alimentación</th><th>Alergia Varias</th><th>Observaciones</th><th>Grupo</th><th>Habitación</th><th>Rol</th></tr>");

            foreach (var persona in listaPersonas)
            {
                htmlTabla.Append("<tr>");
                htmlTabla.Append($"<td>{persona.nombreCompleto}</td>");
                htmlTabla.Append($"<td>{persona.idSexo}</td>");
                htmlTabla.Append($"<td>{persona.documentoId}</td>");
                htmlTabla.Append($"<td>{persona.correo}</td>");
                htmlTabla.Append($"<td>{persona.telefono}</td>");
                htmlTabla.Append($"<td>{persona.alergiaAlimentacion}</td>");
                htmlTabla.Append($"<td>{persona.alegiaVarias}</td>");
                htmlTabla.Append($"<td>{persona.observaciones}</td>");
                htmlTabla.Append($"<td>{persona.idGrupo}</td>");
                htmlTabla.Append($"<td>{persona.idHabitacion}</td>");
                htmlTabla.Append($"<td>{persona.idRol}</td>");
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

        /*Tener listado de Compras
        private List<recetasModel> ObtenerListadoCompras(int idGrupo)
        {
            var listaCompras = new List<recetasModel>();

            // Aquí debes realizar la lógica para obtener los productos relacionados con las recetas del grupo
            // Supongamos que tienes un método en tu capa de datos para obtener las recetas asociadas a un grupo
            // Esto es solo un ejemplo y debes adaptarlo a tu estructura y lógica de datos

            // Obtener las recetas del grupo
            var recetas = new grupoDatos().ObtenerRecetasPorGrupo(idGrupo);

            // Iterar sobre las recetas para obtener los productos asociados
            foreach (var receta in recetas)
            {
                // Supongamos que tienes un método en tu capa de datos para obtener los productos asociados a una receta
                var productosDeReceta = new recetasDatos().ObtenerProductosPorReceta(receta.id);

                // Iterar sobre los productos de la receta y agregarlos al listado de compras
                foreach (var producto in productosDeReceta)
                {
                    listaCompras.Add(new recetasModel { idProducto = producto.idProducto, cantidad = producto.cantidad });
                }
            }

            return listaCompras;
        }*/
        public string ObtenerNombreProductoPorId(int? id, string tabla)
        {
            if (!id.HasValue)
            {
                return string.Empty;
            }

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                string query = $"SELECT nombre FROM {tabla} WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id.Value);

                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
        }
        /*public IActionResult MostrarPDFCompras(int idGrupo)
        {
            // Obtener el listado de compras para el grupo dado
            var listaCompras = ObtenerListadoCompras(idGrupo);

            // Crear una tabla HTML para mostrar el listado de compras
            var htmlTabla = new StringBuilder();
            htmlTabla.Append("<table border='1' cellpadding='10' cellspacing='0'>");
            htmlTabla.Append("<tr><th>Producto</th><th>Cantidad</th></tr>");

            foreach (var compra in listaCompras)
            {
                htmlTabla.Append("<tr>");
                // Aquí obtienes el nombre del producto usando su ID
                var nombreProducto = ObtenerNombreProductoPorId(compra.idProducto, "productos");
                htmlTabla.Append($"<td>{nombreProducto}</td>");
                htmlTabla.Append($"<td>{compra.cantidad}</td>");
                htmlTabla.Append("</tr>");
            }

            htmlTabla.Append("</table>");

            // Convertir el HTML en un archivo PDF
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

            // Descargar el archivo PDF generado
            return File(archivoPDF, "application/pdf");
        }*/
        public IActionResult DescargarPDFGrupo()
        {
            string pagina_actual = HttpContext.Request.Path;
            string url_pagina = HttpContext.Request.GetEncodedUrl();
            url_pagina = url_pagina.Replace(pagina_actual, "");
            url_pagina = $"{url_pagina}/reportes/VistaParaPDFGrupo";

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


        //Mostar pdfs de actividad por grupo
        public IActionResult MostrarPDFActividad()
        {
            var listaActividades = new actividadesDatos().listar();

            var htmlTabla = new StringBuilder();
            htmlTabla.Append("<table border='1' cellpadding='10' cellspacing='0'>");
            htmlTabla.Append("<tr><th>Nombre</th><th>Grupo</th><th>Fecha de Inicio</th><th>Fecha Fin</th><th>recursos</th><th>responsables</th><th>lugarDesde</th><th>Observaciones</th><th>tipo de Actividad</th><th>menu</th><th>Vehiculo</th></tr>");

            foreach (var actividad in listaActividades)
            {
                htmlTabla.Append("<tr>");
                htmlTabla.Append($"<td>{actividad.nombre}</td>");
                htmlTabla.Append($"<td>{actividad.idGrupo}</td>");
                htmlTabla.Append($"<td>{actividad.fechaInicio}</td>");
                htmlTabla.Append($"<td>{actividad.fechaFin}</td>");
                htmlTabla.Append($"<td>{actividad.recursos}</td>");
                htmlTabla.Append($"<td>{actividad.responsables}</td>");
                htmlTabla.Append($"<td>{actividad.lugarDesde}</td>");
                htmlTabla.Append($"<td>{actividad.LugarHacia}</td>");
                htmlTabla.Append($"<td>{actividad.observaciones}</td>");
                htmlTabla.Append($"<td>{actividad.idTipoActividad}</td>");
                htmlTabla.Append($"<td>{actividad.idMenu}</td>");
                htmlTabla.Append($"<td>{actividad.idVehiculo}</td>");
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
    }
}
