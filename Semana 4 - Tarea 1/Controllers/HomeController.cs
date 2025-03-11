// Controllers/HomeController.cs
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestorProyectos.Models;

namespace GestorProyectos.Controllers
{
    /// <summary>
    /// Controlador para la página principal y funciones generales.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Acción que muestra la página principal.
        /// </summary>
        /// <returns>Vista principal</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Acción que muestra la página de política de privacidad.
        /// </summary>
        /// <returns>Vista de política de privacidad</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Acción que muestra información de error.
        /// </summary>
        /// <returns>Vista de error</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}