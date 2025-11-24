using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FabricaNube.Data;

namespace FabricaNube.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoteProduccionController : ControllerBase
    {
        private readonly FabricaDbContext _context;

        public LoteProduccionController(FabricaDbContext context)
        {
            _context = context;
        }

        // GET: api/LoteProduccion
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _context.LotesProduccion
                .Where(l => l.Estado != "BORRADO")
                .ToListAsync();
            return Ok(lista);
        }

        // GET: api/LoteProduccion/{codigo}
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            var l = await _context.LotesProduccion.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (l == null) return NotFound();
            return Ok(l);
        }

        // POST: api/LoteProduccion
        // Registrar lote producido (y notificar/consumir servicio Almacen si se necesita)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoteProduccion dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Codigo))
                dto.Codigo = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

            dto.FechaProduccion = DateTime.UtcNow;
            dto.Estado = "PENDIENTE";

            _context.LotesProduccion.Add(dto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByCodigo), new { codigo = dto.Codigo }, dto);
        }

        // PUT: api/LoteProduccion/{codigo}
        // Editar lote (cantidad, etc.)
        [HttpPut("{codigo}")]
        public async Task<IActionResult> Update(string codigo, [FromBody] LoteProduccion update)
        {
            var l = await _context.LotesProduccion.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (l == null) return NotFound();

            l.CantidadProducida = update.CantidadProducida;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PATCH/PUT estado: api/LoteProduccion/{codigo}/estado
        [HttpPut("{codigo}/estado")]
        public async Task<IActionResult> UpdateEstado(string codigo, [FromBody] string nuevoEstado)
        {
            if (string.IsNullOrWhiteSpace(nuevoEstado)) return BadRequest("Se requiere estado en body");

            var l = await _context.LotesProduccion.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (l == null) return NotFound();

            l.Estado = nuevoEstado.ToUpper();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE (lógico)
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(string codigo)
        {
            var l = await _context.LotesProduccion.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (l == null) return NotFound();

            l.Estado = "BORRADO";

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
