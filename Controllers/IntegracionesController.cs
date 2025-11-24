using FabricaNube.Dtos;
using FabricaNube.Services;
using Microsoft.AspNetCore.Mvc;

namespace FabricaNube.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegracionesController : ControllerBase
    {
        private readonly ISucursalService _sucursalService;

        public IntegracionesController(ISucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        [HttpPut("actualizar-solicitud-sucursal")]
        public async Task<IActionResult> ActualizarSolicitudSucursal(SolicitudActualizarDto dto)
        {
            var ok = await _sucursalService.ActualizarSolicitudAsync(dto);

            if (!ok)
                return BadRequest("Error enviando solicitud al microservicio Sucursal");

            return Ok("Solicitud actualizada correctamente en el microservicio Sucursal");
        }
    }
}

