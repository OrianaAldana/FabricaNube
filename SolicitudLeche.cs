using System.ComponentModel.DataAnnotations;

namespace FabricaNube
{
    public class SolicitudLeche
    {
        [Key]
        public int IdSolicitud { get; set; }

        [Required, MaxLength(30)]
        public string Codigo { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);

        public int CantidadLitros { get; set; }

        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        public string Estado { get; set; } = "PENDIENTE";
    }

}
