using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorProyectos.Models
{
    /// <summary>
    /// Representa una tarea dentro de un proyecto.
    /// Cada tarea pertenece a un proyecto y puede tener múltiples comentarios y archivos.
    /// </summary>
    public class Tarea
    {
        /// <summary>
        /// Identificador único de la tarea (Clave primaria)
        /// </summary>
        [Key]
        public int TareaID { get; set; }

        /// <summary>
        /// Identificador del proyecto al que pertenece esta tarea (Clave foránea)
        /// </summary>
        [Required(ErrorMessage = "Se debe asociar la tarea a un proyecto")]
        public int ProyectoID { get; set; }

        /// <summary>
        /// Título descriptivo de la tarea
        /// </summary>
        [Required(ErrorMessage = "El título de la tarea es obligatorio")]
        [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres")]
        public string Titulo { get; set; }

        /// <summary>
        /// Descripción detallada de la tarea
        /// </summary>
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Estado actual de la tarea (ej: Pendiente, En Progreso, Completada)
        /// </summary>
        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres")]
        public string Estado { get; set; }

        // Propiedades de navegación - Relaciones
        /// <summary>
        /// Proyecto al que pertenece esta tarea
        /// </summary>
        [ForeignKey("ProyectoID")]
        public virtual Proyecto Proyecto { get; set; }

        /// <summary>
        /// Colección de comentarios asociados a esta tarea
        /// </summary>
        public virtual ICollection<Comentario> Comentarios { get; set; }

        /// <summary>
        /// Colección de archivos adjuntos a esta tarea
        /// </summary>
        public virtual ICollection<Archivo> Archivos { get; set; }
    }
}