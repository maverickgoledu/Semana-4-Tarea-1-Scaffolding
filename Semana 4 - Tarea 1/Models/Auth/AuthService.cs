// Services/Auth/AuthService.cs
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using GestorProyectos.Data;
using GestorProyectos.Models;
using GestorProyectos.Models.Auth;

namespace GestorProyectos.Services.Auth
{
    /// <summary>
    /// Implementación del servicio de autenticación.
    /// Gestiona la autenticación de usuarios y las sesiones.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor que recibe las dependencias necesarias a través de inyección
        /// </summary>
        /// <param name="context">Contexto de la base de datos</param>
        /// <param name="httpContextAccessor">Acceso al contexto HTTP</param>
        public AuthService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Autentica a un usuario verificando sus credenciales contra la base de datos
        /// </summary>
        /// <param name="model">Credenciales del usuario</param>
        /// <returns>El usuario autenticado o null si falla la autenticación</returns>
        public async Task<Usuario> AuthenticateAsync(LoginViewModel model)
        {
            // En producción, comparar hash de contraseña en lugar de texto plano
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Contraseña == model.Password);

            return usuario;
        }

        /// <summary>
        /// Crea una sesión para el usuario autenticado usando cookies de autenticación
        /// </summary>
        /// <param name="usuario">Usuario autenticado</param>
        /// <param name="rememberMe">Indica si la sesión debe persistir</param>
        public async Task SignInAsync(Usuario usuario, bool rememberMe)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            // Crear la identidad principal del usuario basada en los claims
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Configurar propiedades de autenticación
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe
                    ? DateTimeOffset.UtcNow.AddDays(14) // Dos semanas si "recordarme" está activado
                    : DateTimeOffset.UtcNow.AddHours(2) // Dos horas por defecto
            };

            // Iniciar sesión
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        /// <summary>
        /// Cierra la sesión del usuario actual
        /// </summary>
        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}