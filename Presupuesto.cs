using System.ComponentModel.DataAnnotations;

namespace FabricaNube
{
    public class Presupuesto
    {
        [Key]
        public int IdPresupuesto { get; set; }

        [Required, MaxLength(30)]
        public string Codigo { get; set; } = string.Empty;

        public decimal MontoSolicitado { get; set; }

        public string Motivo { get; set; } = string.Empty;

        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        public string Estado { get; set; } = "PENDIENTE";
    }

}
