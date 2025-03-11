using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestorProyectos.Models.Auth;
using GestorProyectos.Services.Auth;

namespace GestorProyectos.Controllers
{
    /// <summary>
    /// Controlador responsable de la gestión de cuentas de usuario y autenticación.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor que recibe el servicio de autenticación mediante inyección de dependencias.
        /// </summary>
        /// <param name="authService">Servicio de autenticación</param>
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Acción que muestra la vista de inicio de sesión.
        /// </summary>
        /// <param name="returnUrl">URL a la que redireccionar después del login</param>
        /// <returns>Vista de inicio de sesión</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Acción que procesa el formulario de inicio de sesión.
        /// </summary>
        /// <param name="model">Datos del formulario de login</param>
        /// <param name="returnUrl">URL a la que redireccionar después del login</param>
        /// <returns>Redirección a la página solicitada o a la página principal</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            // Validar el modelo recibido
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Autenticar al usuario
            var usuario = await _authService.AuthenticateAsync(model);
            if (usuario == null)
            {
                // Autenticación fallida
                ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                return View(model);
            }

            // Iniciar sesión
            await _authService.SignInAsync(usuario, model.RememberMe);

            // Redireccionar
            return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /// <summary>
        /// Acción que cierra la sesión del usuario.
        /// </summary>
        /// <returns>Redirección a la página principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /// <summary>
        /// Acción que muestra un mensaje de acceso denegado.
        /// </summary>
        /// <returns>Vista de acceso denegado</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}