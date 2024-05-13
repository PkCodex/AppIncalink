﻿using AppIncalink.Datos;
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
    }
}