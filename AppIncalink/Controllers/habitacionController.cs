using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppIncalink.Controllers
{
    public class habitacionController : Controller
    {
        habitacionDatos _habitacionDatos = new habitacionDatos();
        public IActionResult Listar()
        {
            //La vista mostrara una lista
            var oLista = _habitacionDatos.listar();
            return View(oLista);
        }


        public IActionResult Guardar()
        {
            //metodo que devuleve la vista
            return View();
        }

        //
        [HttpPost]
        public IActionResult Guardar(habitacionModel oHabitacion)
        {
            //Metodo recibe el obejto para guardarlo en bd
            if (!ModelState.IsValid)
                return View();
            var respuesta = _habitacionDatos.Guardar(oHabitacion);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();
        }
        public IActionResult Editar(int id)
        {
            //metodo que devuleve la vista
            var ohabitacion = _habitacionDatos.Obtener(id);
            return View(ohabitacion);
        }
        [HttpPost]
        public IActionResult Editar(habitacionModel ohabitacion)
        {
            if (!ModelState.IsValid)
                return View();
            var respuesta = _habitacionDatos.Editar(ohabitacion);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();
        }

        //Metodo de eliminar
        public IActionResult Eliminar(int id)
        {
            //metodo que devuleve la vista
            var ohabitacion = _habitacionDatos.Obtener(id);
            return View(ohabitacion);
        }
        [HttpPost]
        public IActionResult Eliminar(habitacionModel ohabitacion)
        {

            var respuesta = _habitacionDatos.Eliminar(ohabitacion.id);
            if (respuesta)

                return RedirectToAction("Listar");
            else
                return View();
        }
    }

   
}
