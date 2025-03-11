using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorProyectos.Models
{
    /// <summary>
    /// Representa un archivo adjunto a una tarea específica.
    /// </summary>
    public class Archivo
    {
        /// <summary>
        /// Identificador único del archivo (Clave primaria)
        /// </summary>
        [Key]
        public int ArchivoID { get; set; }

        /// <summary>
        /// Identificador de la tarea a la que pertenece este archivo (Clave foránea)
        /// </summary>
        [Required(ErrorMessage = "Se debe asociar el archivo a una tarea")]
        public int TareaID { get; set; }

        /// <summary>
        /// Nombre del archivo
        /// </summary>
        [Required(ErrorMessage = "El nombre del archivo es obligatorio")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
        public string Nombre { get; set; }

        /// <summary>
        /// Ruta donde se almacena físicamente el archivo
        /// </summary>
        [Required(ErrorMessage = "La ruta es obligatoria")]
        [StringLength(500, ErrorMessage = "La ruta no puede exceder los 500 caracteres")]
        public string Ruta { get; set; }

        /// <summary>
        /// Tipo de archivo (ej: PDF, Word, Excel, Imagen)
        /// </summary>
        [Required(ErrorMessage = "El tipo de archivo es obligatorio")]
        [StringLength(50, ErrorMessage = "El tipo no puede exceder los 50 caracteres")]
        public string TipoArchivo { get; set; }

        // Propiedades de navegación - Relaciones
        /// <summary>
        /// Tarea a la que pertenece este archivo
        /// </summary>
        [ForeignKey("TareaID")]
        public virtual Tarea Tarea { get; set; }
    }
}