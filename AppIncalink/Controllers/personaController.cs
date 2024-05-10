using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace AppIncalink.Controllers
{
    public class personaController : Controller
    {
        personaDatos _personaDatos = new personaDatos();

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
        public string ObtenerId(int? id, string tabla)
        {
            if (!id.HasValue)
            {
                return string.Empty;
            }

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                string query = $"SELECT * FROM {tabla} WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id.Value);

                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
        }

        public IActionResult Listar()
        {
            //La vista mostrara una lista
            var oLista = _personaDatos.listar();
            return View(oLista);
        }

        //Listar Grupo en mi Select

        private List<SelectListItem> GetSexoOptions()
        {
            var sexoOptions = new List<SelectListItem>
             {
                new SelectListItem { Value = "1", Text = "Masculino" },
                new SelectListItem { Value = "2", Text = "Femenino" }
            };
            return sexoOptions;
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

        //listar habitacion para mi select
        private List<SelectListItem> GetHabitacionOptions()
        {
            var habitacionDatos = new habitacionDatos();
            var habitacion = habitacionDatos.listar();
            var grupoOptions = new List<SelectListItem>();
            foreach (var habitaciones in habitacion)
            {
                grupoOptions.Add(new SelectListItem
                {
                    Text = habitaciones.numCamas.ToString(),
                    Value = habitaciones.id.ToString()
                });
            }
            return grupoOptions;
        }

        //Listar rol para mi select
        private List<SelectListItem> GetRolOptions()
        {
            var rolDatos = new rolDatos();
            var rol = rolDatos.listar();
            var rolOptions = new List<SelectListItem>();
            foreach (var roles in rol)
            {
                rolOptions.Add(new SelectListItem
                {
                    Text = roles.nombre,
                    Value = roles.id.ToString()
                });
            }
            return rolOptions;
        }




        public IActionResult Guardar(int id)
        {

            var persona = _personaDatos.Obtener(id);

            if (persona == null)
            {
                return NotFound(); // Retornar una vista de error 404 si la persona no se encuentra
            }

            // Obtener los nombres correspondientes a los IDs para mostrar en los SelectLists
            var nombreSexo = ObtenerNombrePorId(persona.idSexo, "sexo");
            var nombreGrupo = ObtenerNombrePorId(persona.idGrupo, "grupo");
            var numeroHabitacion = ObtenerId(persona.idHabitacion, "habitacion");
            var nombreRol = ObtenerNombrePorId(persona.idRol, "rol");
            ViewBag.SexoOptions = new SelectList(GetSexoOptions(), "Value", "Text", persona.idSexo);
            ViewBag.GrupoOptions = new SelectList(GetGrupoOptions(), "Value", "Text", persona.idGrupo);
            ViewBag.HabitacionOptions = new SelectList(GetHabitacionOptions(), "Value", "Text", persona.idHabitacion);
            ViewBag.RolOptions = new SelectList(GetRolOptions(), "Value", "Text", persona.idRol);
            ViewBag.NombreSexo = nombreSexo;
            ViewBag.NombreGrupo = nombreGrupo;
            ViewBag.NumeroHabitacion = numeroHabitacion;
            ViewBag.NombreRol = nombreRol;

            return View(persona);
        }
        [HttpPost]
        public IActionResult Guardar(personaModel oPersona)
        {
            //Metodo recibe el obejeto para guardarlo en bd
            if (!ModelState.IsValid)
                return View();
            var respuesta = _personaDatos.Guardar(oPersona);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();

        }
        //Metodo de editar perdonas
        public IActionResult Editar(int id)
        {
            var opersona = _personaDatos.Obtener(id);

            var grupoOptions = GetGrupoOptions();

            if (grupoOptions != null && grupoOptions.Any())
            {
                ViewBag.GrupoOptions = new SelectList(grupoOptions, "Value", "Text");
            }
            else
            {
                ViewBag.GrupoOptions = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Sin grupos disponibles" } };
            }

            // Asignar las opciones de los SelectList a ViewBag
            ViewBag.SexoOptions = new SelectList(GetSexoOptions(), "Value", "Text", opersona.idSexo);
            // ViewBag.GrupoOptions = new SelectList(GetGrupoOptions(), "Value", "Text", opersona.idGrupo);
            ViewBag.HabitacionOptions = new SelectList(GetHabitacionOptions(), "Value", "Text", opersona.idHabitacion);
            ViewBag.RolOptions = new SelectList(GetRolOptions(), "Value", "Text", opersona.idRol);

            return View(opersona);
        }
        [HttpPost]
        public IActionResult Editar(personaModel opersona)
        {
            if (!ModelState.IsValid)
                return View();
            var respuesta = _personaDatos.Editar(opersona);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();
        }

        public IActionResult Eliminar(int id)
        {
            //metodo que devuleve la vista
            var opersona = _personaDatos.Obtener(id);
            return View(opersona);
        }
        [HttpPost]
        public IActionResult Eliminar(personaModel opersona)
        {

            var respuesta = _personaDatos.Eliminar(opersona.id);
            if (respuesta)

                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
