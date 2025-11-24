using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FabricaNube.Data;

namespace FabricaNube.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private readonly FabricaDbContext _context;

        public PresupuestoController(FabricaDbContext context)
        {
            _context = context;
        }

        // GET: api/presupuesto
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _context.Presupuestos
                .Where(p => p.Estado != "BORRADO")
                .ToListAsync();
            return Ok(lista);
        }

        // GET: api/presupuesto/{codigo}
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            var p = await _context.Presupuestos.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (p == null) return NotFound();
            return Ok(p);
        }

        // POST: api/presupuesto
        // Enviar presupuesto a Contabilidad
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Presupuesto dto)
        {
            // generar codigo si no viene
            if (string.IsNullOrWhiteSpace(dto.Codigo))
                dto.Codigo = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

            dto.FechaSolicitud = DateTime.UtcNow;
            dto.Estado = "PENDIENTE";

            _context.Presupuestos.Add(dto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByCodigo), new { codigo = dto.Codigo }, dto);
        }

        // PUT: api/presupuesto/{codigo}
        // Editar datos generales (no estado)
        [HttpPut("{codigo}")]
        public async Task<IActionResult> Update(string codigo, [FromBody] Presupuesto update)
        {
            var p = await _context.Presupuestos.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (p == null) return NotFound();

            // actualizar campos permitidos
            p.MontoSolicitado = update.MontoSolicitado;
            p.Motivo = update.Motivo;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/presupuesto/{codigo}/estado
        // Cambiar solo el estado (aprobado/rechazado/etc)
        [HttpPut("{codigo}/estado")]
        public async Task<IActionResult> UpdateEstado(string codigo, [FromBody] string nuevoEstado)
        {
            if (string.IsNullOrWhiteSpace(nuevoEstado))
                return BadRequest("Se requiere estado en el body.");

            var p = await _context.Presupuestos.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (p == null) return NotFound();

            p.Estado = nuevoEstado.ToUpper();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE (lógico): api/presupuesto/{codigo}
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(string codigo)
        {
            var p = await _context.Presupuestos.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (p == null) return NotFound();

            p.Estado = "BORRADO";

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

