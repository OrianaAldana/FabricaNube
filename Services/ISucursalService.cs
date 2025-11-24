using FabricaNube.Dtos;

    namespace FabricaNube.Services
    {
        public interface ISucursalService
        {
            Task<bool> ActualizarSolicitudAsync(SolicitudActualizarDto dto);
        }
    }
