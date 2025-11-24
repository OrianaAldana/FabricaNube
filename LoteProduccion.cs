using System.ComponentModel.DataAnnotations;

namespace FabricaNube
{
    public class LoteProduccion
    {
        [Key]
        public int IdLote { get; set; }

        [Required, MaxLength(30)]
        public string Codigo { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);

        public int CantidadProducida { get; set; }

        public DateTime FechaProduccion { get; set; } = DateTime.UtcNow;

        [MaxLength(20)]
        public string Estado { get; set; } = "PENDIENTE";
    }

}
