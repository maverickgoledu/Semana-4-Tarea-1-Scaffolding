// Controllers/HomeController.cs
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestorProyectos.Models;

namespace GestorProyectos.Controllers
{
    /// <summary>
    /// Controlador para la p�gina principal y funciones generales.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Acci�n que muestra la p�gina principal.
        /// </summary>
        /// <returns>Vista principal</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Acci�n que muestra la p�gina de pol�tica de privacidad.
        /// </summary>
        /// <returns>Vista de pol�tica de privacidad</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Acci�n que muestra informaci�n de error.
        /// </summary>
        /// <returns>Vista de error</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}