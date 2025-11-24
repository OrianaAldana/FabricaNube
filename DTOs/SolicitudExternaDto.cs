namespace FabricaNube.DTOs
{
    public class SolicitudExternaDto
    {
        public Guid IdSolicitud { get; set; }
        public string NuevoEstado { get; set; } = "";
        public string CiAprobador { get; set; } = "";
        public string MotivoRechazo { get; set; } = "";
    }

}
