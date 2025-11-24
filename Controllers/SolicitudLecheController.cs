using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FabricaNube.Data;

namespace FabricaNube.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudLecheController : ControllerBase
    {
        private readonly FabricaDbContext _context;

        public SolicitudLecheController(FabricaDbContext context)
        {
            _context = context;
        }

        // GET: api/solicitudleche
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _context.SolicitudesLeche
                .Where(s => s.Estado != "BORRADO")
                .ToListAsync();
            return Ok(lista);
        }

        // GET: api/solicitudleche/{codigo}
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            var s = await _context.SolicitudesLeche.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (s == null) return NotFound();
            return Ok(s);
        }

        // POST: api/solicitudleche
        // Enviar solicitud a la Granja
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SolicitudLeche dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Codigo))
                dto.Codigo = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

            dto.FechaSolicitud = DateTime.UtcNow;
            dto.Estado = "PENDIENTE";

            _context.SolicitudesLeche.Add(dto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByCodigo), new { codigo = dto.Codigo }, dto);
        }

        // PUT: api/solicitudleche/{codigo}
        // Editar solicitud (cantidad por ejemplo)
        [HttpPut("{codigo}")]
        public async Task<IActionResult> Update(string codigo, [FromBody] SolicitudLeche update)
        {
            var s = await _context.SolicitudesLeche.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (s == null) return NotFound();

            s.CantidadLitros = update.CantidadLitros;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/solicitudleche/{codigo}/estado
        // La Granja usará este endpoint para marcar ENVIADO/RECIBIDO etc.
        [HttpPut("{codigo}/estado")]
        public async Task<IActionResult> UpdateEstado(string codigo, [FromBody] string nuevoEstado)
        {
            if (string.IsNullOrWhiteSpace(nuevoEstado)) return BadRequest("Se requiere estado en body");

            var s = await _context.SolicitudesLeche.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (s == null) return NotFound();

            s.Estado = nuevoEstado.ToUpper();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE (lógico)
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(string codigo)
        {
            var s = await _context.SolicitudesLeche.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (s == null) return NotFound();

            s.Estado = "BORRADO";

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
