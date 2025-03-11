using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace GestorProyectos.Models
{
    /// <summary>
    /// Representa un proyecto en el sistema.
    /// Cada proyecto puede tener múltiples tareas asociadas.
    /// </summary>
    public class Proyecto
    {
        /// <summary>
        /// Identificador único del proyecto (Clave primaria)
        /// </summary>
        [Key]
        public int ProyectoID { get; set; }

        /// <summary>
        /// Nombre del proyecto
        /// </summary>
        [Required(ErrorMessage = "El nombre del proyecto es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción detallada del proyecto
        /// </summary>
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha de inicio del proyecto
        /// </summary>
        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Estado actual del proyecto (ej: Planificado, En Progreso, Completado)
        /// </summary>
        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres")]
        public string Estado { get; set; }

        // Propiedades de navegación - Relaciones
        /// <summary>
        /// Colección de tareas asociadas a este proyecto
        /// </summary>
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}