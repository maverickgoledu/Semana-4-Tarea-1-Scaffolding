using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorProyectos.Models
{
    /// <summary>
    /// Representa un comentario asociado a una tarea específica y creado por un usuario.
    /// </summary>
    public class Comentario
    {
        /// <summary>
        /// Identificador único del comentario (Clave primaria)
        /// </summary>
        [Key]
        public int ComentarioID { get; set; }

        /// <summary>
        /// Identificador de la tarea a la que pertenece este comentario (Clave foránea)
        /// </summary>
        [Required(ErrorMessage = "Se debe asociar el comentario a una tarea")]
        public int TareaID { get; set; }

        /// <summary>
        /// Identificador del usuario que creó este comentario (Clave foránea)
        /// </summary>
        [Required(ErrorMessage = "Se debe asociar el comentario a un usuario")]
        public int UsuarioID { get; set; }

        /// <summary>
        /// Contenido del comentario
        /// </summary>
        [Required(ErrorMessage = "El contenido del comentario es obligatorio")]
        [StringLength(1000, ErrorMessage = "El contenido no puede exceder los 1000 caracteres")]
        public string Contenido { get; set; }

        /// <summary>
        /// Fecha y hora en que se creó el comentario
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

        // Propiedades de navegación - Relaciones
        /// <summary>
        /// Tarea a la que pertenece este comentario
        /// </summary>
        [ForeignKey("TareaID")]
        public virtual Tarea Tarea { get; set; }

        /// <summary>
        /// Usuario que creó este comentario
        /// </summary>
        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
    }
}