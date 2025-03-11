using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorProyectos.Models
{
    /// <summary>
    /// Representa un usuario en el sistema.
    /// Cada usuario puede tener varios roles y estar relacionado con múltiples proyectos y comentarios.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario (Clave primaria)
        /// </summary>
        [Key]
        public int UsuarioID { get; set; }

        /// <summary>
        /// Nombre completo del usuario
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        /// <summary>
        /// Correo electrónico del usuario (usado para login)
        /// </summary>
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(150, ErrorMessage = "El email no puede exceder los 150 caracteres")]
        public string Email { get; set; }

        /// <summary>
        /// Contraseña del usuario (debe almacenarse hasheada)
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255, ErrorMessage = "La contraseña debe tener entre 8 y 255 caracteres", MinimumLength = 8)]
        public string Contraseña { get; set; }

        /// <summary>
        /// Rol del usuario en el sistema (ej: Administrador, Usuario, Gestor)
        /// </summary>
        [Required(ErrorMessage = "El rol es obligatorio")]
        [StringLength(50, ErrorMessage = "El rol no puede exceder los 50 caracteres")]
        public string Rol { get; set; }

        // Propiedades de navegación - Relaciones
        /// <summary>
        /// Colección de comentarios realizados por este usuario
        /// </summary>
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}