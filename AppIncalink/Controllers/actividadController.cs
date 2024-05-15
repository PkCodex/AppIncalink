using AppIncalink.Models;
using AppIncalink.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace AppIncalink.Controllers
{
    public class actividadController : Controller
    {
        actividadesDatos _actividadesDatos = new actividadesDatos();
        public IActionResult Listar()
        {
            //La vista mostrara una lista
            var oLista = _actividadesDatos.listar();
            return View(oLista);
        }

        private List<SelectListItem> GetGrupoOptions()
        {
            var grupoDatos = new grupoDatos();
            var grupos = grupoDatos.listar();
            var grupoOptions = new List<SelectListItem>();
            foreach (var grupo in grupos)
            {
                grupoOptions.Add(new SelectListItem
                {
                    Text = grupo.nombre,
                    Value = grupo.id.ToString()
                });
            }
            return grupoOptions;
        }

        public string ObtenerNombrePorId(int? id, string tabla)
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

        public string ObtenerlugarPorId(int? id, string tabla)
        {
            if (!id.HasValue)
            {
                return string.Empty;
            }

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                string query = $"SELECT lugar FROM {tabla} WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id.Value);

                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
        }

        private List<SelectListItem> GetTipoActividadOptions()
        {
            var tipoActividadOptions = new List<SelectListItem>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                string query = "SELECT id, nombre FROM tipoActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        tipoActividadOptions.Add(new SelectListItem
                        {
                            Value = dr["id"].ToString(),
                            Text = dr["nombre"].ToString()
                        });
                    }
                }
            }

            return tipoActividadOptions;
        }
        private List<SelectListItem> GetMenuOptions()
        {
            var menuOptions = new List<SelectListItem>();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                string query = "SELECT id, nombre FROM menu";
                SqlCommand cmd = new SqlCommand(query, conexion);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        menuOptions.Add(new SelectListItem
                        {
                            Value = dr["id"].ToString(),
                            Text = dr["nombre"].ToString()
                        });
                    }
                }
            }

            return menuOptions;
        }

        private List<SelectListItem> GetVehiculoOptions()
        {
            var vehiculoOptions = new List<SelectListItem>();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                string query = "SELECT id, lugar FROM vehiculo";
                SqlCommand cmd = new SqlCommand(query, conexion);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        vehiculoOptions.Add(new SelectListItem
                        {
                            Value = dr["id"].ToString(),
                            Text = dr["lugar"].ToString()
                        });
                    }
                }
            }

            return vehiculoOptions;
        }
        //Metodo guardar
        public IActionResult Guardar(int id)
        {
            //metodo que devuleve la vista
            var actividad = _actividadesDatos.Obtener(id);

            if (actividad == null)
            {
                return NotFound(); // Retornar una vista de error 404 si la actividad no se encuentra
            }

            // Obtener los nombres correspondientes a los IDs para mostrar en los SelectLists y los nombres de las tablas
            var nombreGrupo = ObtenerNombrePorId(actividad.idGrupo, "grupo");
            var nombreTipoActividad = ObtenerNombrePorId(actividad.idTipoActividad, "tipoActividad");
            var nombreMenu = ObtenerNombrePorId(actividad.idMenu, "menu");
            var nombreTransporte = ObtenerlugarPorId(actividad.idVehiculo, "vehiculo");

            ViewBag.GrupoOptions = new SelectList(GetGrupoOptions(), "Value", "Text", actividad.idGrupo);
            ViewBag.MenuOptions = new SelectList(GetMenuOptions(), "Value", "Text");
            ViewBag.tipoActividadOptions = new SelectList(GetTipoActividadOptions(), "Value", "Text");
            ViewBag.vehiculoOptions = new SelectList(GetVehiculoOptions(), "Value", "Text");
            ViewBag.NombreGrupo = nombreGrupo;
            ViewBag.NombreTipoActividad = nombreTipoActividad;
            ViewBag.NombreMenu = nombreMenu;
            ViewBag.NombreTransporte = nombreTransporte;


            return View(actividad);
        }

        [HttpPost]
        public IActionResult Guardar(actividadesModel oActividad)
        {
            //Metodo recibe el obejeto para guardarlo en bd
            if (!ModelState.IsValid)
                return View();
            var respuesta = _actividadesDatos.Guardar(oActividad);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();
        }


        [HttpGet]
        public IActionResult GetEvents()
        {
            var eventos = _actividadesDatos.listar(); // Asume que tienes un método para listar actividades

            var eventosCalendario = eventos.Select(e => new
            {
                title = e.nombre,
                start = e.fechaInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = e.fechaFin.ToString("yyyy-MM-ddTHH:mm:ss")
            }).ToList();

            return Json(eventosCalendario);
        }



        public IActionResult Eliminar(int id)
        {
            // Método que devuelve la vista
            var oactividades = _actividadesDatos.Obtener(id);
            return View(oactividades);
        }

        [HttpPost]
        public IActionResult Eliminar(actividadesModel oactividades)
        {
            // Verificar si el valor de idTipoActividad es DBNull antes de eliminar
            if (DBNull.Value.Equals(oactividades.idTipoActividad))
            {
                ModelState.AddModelError("", "El campo idTipoActividad es nulo.");
                return View(oactividades); // Retorna la vista con el mensaje de error
            }

            var respuesta = _actividadesDatos.Eliminar(oactividades.id);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                ModelState.AddModelError("", "Hubo un error al intentar eliminar la actividad.");
                return View(oactividades); // Retorna la vista con el mensaje de error
            }
        }
    }
}
