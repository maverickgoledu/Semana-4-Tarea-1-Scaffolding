namespace GestorProyectos.Models
{
    /// <summary>
    /// Modelo para mostrar información de error.
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}