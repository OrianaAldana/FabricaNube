using System.ComponentModel.DataAnnotations;

namespace FabricaNube
{
    public class LoteProduccion
    {
        [Key]
        public int IdLote { get; set; }

        [Required, MaxLength(30)]
        public string Codigo { get; set; } = string.Empty;

        public int CantidadProducida { get; set; }

        public DateTime FechaProduccion { get; set; } = DateTime.UtcNow;

        [MaxLength(20)]
        public string Estado { get; set; } = "PENDIENTE";
    }

}
