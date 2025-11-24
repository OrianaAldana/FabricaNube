namespace FabricaNube.Dtos
{
    public class SolicitudActualizarDto
    {
        public Guid IdSolicitud { get; set; }
        public string NuevoEstado { get; set; } = string.Empty;
        public string CiAprobador { get; set; } = string.Empty;
        public string MotivoRechazo { get; set; } = string.Empty;
    }
}

