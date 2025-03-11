namespace GestorProyectos.Models
{
    /// <summary>
    /// Modelo para mostrar informaci�n de error.
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}