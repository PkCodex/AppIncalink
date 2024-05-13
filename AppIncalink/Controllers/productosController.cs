using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace AppIncalink.Controllers
{
    public class productosController : Controller
    {
        productosDatos _productosDatos = new productosDatos();
        public IActionResult Listar()
        {
            //La vista mostrara una lista
            var oLista = _productosDatos.listar();
            return View(oLista);
        }


        public IActionResult Guardar(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(productoModel oProducto)
        {
            //Metodo recibe el obejto para guardarlo en bd
            if (!ModelState.IsValid)
                return View();
            var respuesta = _productosDatos.Guardar(oProducto);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();

        }

        public IActionResult Eliminar(int id)
        {
            //metodo que devuleve la vista
            var ohabitacion = _productosDatos.Obtener(id);
            return View(ohabitacion);
        }
        [HttpPost]
        public IActionResult Eliminar(habitacionModel ohabitacion)
        {

            var respuesta = _productosDatos.Eliminar(ohabitacion.id);
            if (respuesta)

                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
