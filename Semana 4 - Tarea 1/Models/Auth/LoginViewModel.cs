using System.ComponentModel.DataAnnotations;

namespace GestorProyectos.Models.Auth
{
    /// <summary>
    /// Modelo de vista para el formulario de inicio de sesión.
    /// Contiene las propiedades necesarias para el proceso de autenticación.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Correo electrónico del usuario (usado como nombre de usuario)
        /// </summary>
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        /// <summary>
        /// Indica si el usuario desea que su sesión sea recordada
        /// </summary>
        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}