using System.ComponentModel.DataAnnotations;

namespace FabricaNube
{
    public class SolicitudLeche
    {
        [Key]
        public int IdSolicitud { get; set; }

        [Required, MaxLength(30)]
        public string Codigo { get; set; } = string.Empty;

        public int CantidadLitros { get; set; }

        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        public string Estado { get; set; } = "PENDIENTE";
    }

}
