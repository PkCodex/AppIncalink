using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace AppIncalink.Controllers
{
    public class menuController : Controller
    {
        menuDatos _menuDatos = new menuDatos();
        public IActionResult Listar()
        {
            //La vista mostrara una lista
            var oLista = _menuDatos.listar();
            return View(oLista);
        }

        public IActionResult Guardar()
        {
            //metodo que devuleve la vista
            return View();
        }

        //
        [HttpPost]
        public IActionResult Guardar(menuModel oMenu)
        {
            //Metodo recibe el obejto para guardarlo en bd
            if (!ModelState.IsValid)
                return View();
            var respuesta = _menuDatos.Guardar(oMenu);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();
        }

        public IActionResult Editar(int id)
        {
            //metodo que devuleve la vista
            var omenu = _menuDatos.Obtener(id);
            return View(omenu);
        }
        [HttpPost]
        public IActionResult Editar(menuModel omenu)
        {
            if (!ModelState.IsValid)
                return View();
            var respuesta = _menuDatos.Editar(omenu);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();
        }


        public IActionResult Eliminar(int id)
        {
            //metodo que devuleve la vista
            var omenu = _menuDatos.Obtener(id);
            return View(omenu);
        }
        [HttpPost]
        public IActionResult Eliminar(menuModel omenu)
        {

            var respuesta = _menuDatos.Eliminar(omenu.id);
            if (respuesta)

                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
