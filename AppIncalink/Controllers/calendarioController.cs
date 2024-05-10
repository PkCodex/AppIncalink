using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace AppIncalink.Controllers
{
    public class calendarioController : Controller
    {
        private readonly actividadesDatos _actividadesDatos;
        private readonly grupoDatos _gruposDatos;
        public ViewResult Calendario()
        {
            return View();
        }
        public calendarioController(actividadesDatos actividadesDatos, grupoDatos grupoDatos)
        {
            _actividadesDatos = actividadesDatos;
            _gruposDatos = grupoDatos;
        }
        public JsonResult GetEvents()
        {

            var eventosActividades = _actividadesDatos.listar(); // Obtener eventos de actividades desde la capa de datos
            var eventosGrupos = _gruposDatos.listar(); // Obtener eventos de grupos desde la capa de datos

            var eventosJsonActividades = eventosActividades.Select(e => new
            {
                title = e.nombre,
                start = e.fechaInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = e.fechaFin.ToString("yyyy-MM-ddTHH:mm:ss"),
                location = e.lugarDesde, // O cualquier otro campo que desees usar como ubicación
                url = "", // URL opcional para más detalles del evento
                color = "blue"
            }).ToList();

            var eventosJsonGrupos = eventosGrupos.Select(g => new
            {
                title = $"Grupo: {g.nombre}",
                start = g.fechaLlegada.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = g.fechaSalida.ToString("yyyy-MM-ddTHH:mm:ss"),
                location = "", // Puedes dejarlo vacío o agregar información adicional si lo deseas
                url = "", // URL opcional para más detalles del evento
                color = "green"
            }).ToList();

            // Combinar eventos de actividades y grupos en una sola lista
            var todosEventosJson = eventosJsonActividades.Concat(eventosJsonGrupos).ToList();

            return Json(todosEventosJson);

        }
    }
}
