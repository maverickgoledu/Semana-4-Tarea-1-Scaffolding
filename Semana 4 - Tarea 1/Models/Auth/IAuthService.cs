// Services/Auth/IAuthService.cs
using System.Threading.Tasks;
using GestorProyectos.Models;
using GestorProyectos.Models.Auth;

namespace GestorProyectos.Services.Auth
{
    /// <summary>
    /// Interfaz que define los métodos necesarios para el servicio de autenticación
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Autentica a un usuario basado en sus credenciales
        /// </summary>
        /// <param name="model">Modelo con las credenciales del usuario</param>
        /// <returns>El usuario autenticado o null si la autenticación falla</returns>
        Task<Usuario> AuthenticateAsync(LoginViewModel model);

        /// <summary>
        /// Crea una sesión para el usuario autenticado
        /// </summary>
        /// <param name="usuario">Usuario autenticado</param>
        /// <param name="rememberMe">Indica si la sesión debe ser persistente</param>
        /// <returns>Task que representa la operación asincrónica</returns>
        Task SignInAsync(Usuario usuario, bool rememberMe);

        /// <summary>
        /// Cierra la sesión del usuario actual
        /// </summary>
        /// <returns>Task que representa la operación asincrónica</returns>
        Task SignOutAsync();
    }
}