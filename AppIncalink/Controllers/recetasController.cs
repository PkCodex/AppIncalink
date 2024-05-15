using Microsoft.AspNetCore.Mvc;

using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;


namespace AppIncalink.Controllers
{
    public class recetasController : Controller
    {
        recetasDatos _recetasDatos = new recetasDatos();
        public IActionResult Listar()
        {
            //La vista mostrara una lista
            var oLista = _recetasDatos.listar();
            return View(oLista);
        }


        private List<SelectListItem> GetMenuOptions()
        {
            var menuDatos = new menuDatos();
            var menus = menuDatos.listar();
            var menuOptions = new List<SelectListItem>();
            foreach (var menu in menus)
            {
                menuOptions.Add(new SelectListItem
                {
                    Text = menu.nombre,
                    Value = menu.id.ToString()
                });
            }
            return menuOptions;

        }

        private List<SelectListItem> GetProductosOptions()
        {
            var productoDatos = new productosDatos();
            var productos = productoDatos.listar();
            var productoOptions = new List<SelectListItem>();
            foreach (var grupo in productos)
            {
                productoOptions.Add(new SelectListItem
                {
                    Text = grupo.nombre,
                    Value = grupo.id.ToString()
                });
            }
            return productoOptions;
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

        public IActionResult Guardar(int id)
        {

            var recetas = _recetasDatos.Obtener(id);

            if (recetas == null)
            {
                return NotFound(); // Retornar una vista de error 404 si la persona no se encuentra
            }

            // Obtener los nombres correspondientes a los IDs para mostrar en los SelectLists
            var nombreMenu = ObtenerNombrePorId(recetas.idMenu, "menu");
            var nombreProducto = ObtenerNombrePorId(recetas.idProducto, "productos");

            ViewBag.MenuOptions = new SelectList(GetMenuOptions(), "Value", "Text", recetas.idMenu);
            ViewBag.ProductosOptions = new SelectList(GetProductosOptions(), "Value", "Text", recetas.idMenu);
            ViewBag.NombreMenu = nombreMenu;
            ViewBag.NombreProductos = nombreProducto;


            return View(recetas);
        }
        [HttpPost]
        public IActionResult Guardar(recetasModel oRecetas)
        {
            //Metodo recibe el obejeto para guardarlo en bd
            if (!ModelState.IsValid)
                return View();
            var respuesta = _recetasDatos.Guardar(oRecetas);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();

        }

    }
}
