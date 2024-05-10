using AppIncalink.Datos;
using AppIncalink.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppIncalink.Controllers
{
    public class grupoController : Controller
    {
        grupoDatos _grupoDatos = new grupoDatos();
        personaDatos _personaDatos = new personaDatos();
        public IActionResult Listar()
        {
            //La vista mostrara una lista
            var oLista = _grupoDatos.listar();
            return View(oLista);
        }
        public IActionResult Guardar()
        {
            //metodo que devuleve la vista
            return View();
        }

        //
        [HttpPost]
        public IActionResult Guardar(grupoModel oGrupo)
        {
            //Metodo recibe el obejto para guardarlo en bd
            if (!ModelState.IsValid)
                return View();
            var respuesta = _grupoDatos.Guardar(oGrupo);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();
        }

        public IActionResult Editar(int id)
        {
            //metodo que devuleve la vista
            var ohabitacion = _grupoDatos.Obtener(id);
            return View(ohabitacion);
        }
        [HttpPost]
        public IActionResult Editar(grupoModel oGrupo)
        {
            if (!ModelState.IsValid)
                return View();
            var respuesta = _grupoDatos.Editar(oGrupo);
            if (respuesta)

                return RedirectToAction("Listar");
            else return View();
        }

        
        public IActionResult ListarPersonas(int idGrupo)
        {
            var personas = _grupoDatos.ObtenerPersonasPorGrupo(idGrupo);
            return View(personas);
        }

        public IActionResult Eliminar(int id)
        {
            //metodo que devuleve la vista
            var ogrupo = _grupoDatos.Obtener(id);
            return View(ogrupo);
        }
        [HttpPost]
        public IActionResult Eliminar(grupoModel ogrupo)
        {

            var respuesta = _grupoDatos.Eliminar(ogrupo.id);
            if (respuesta)

                return RedirectToAction("Listar");
            else
                return View();
        }
    }

}
