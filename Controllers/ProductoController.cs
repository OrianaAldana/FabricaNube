using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FabricaNube.Data;

namespace FabricaNube.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly FabricaDbContext _context;

        public ProductoController(FabricaDbContext context)
        {
            _context = context;
        }

        // GET api/producto
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _context.Productos
                .Where(p => p.Estado != "BORRADO")
                .ToListAsync();

            return Ok(productos);
        }

        // GET api/producto/{codigo}
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetByCode(string codigo)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Codigo == codigo);

            return producto == null ? NotFound() : Ok(producto);
        }

        // POST api/producto
        [HttpPost]
        public async Task<IActionResult> Create(Producto dto)
        {
            dto.Codigo = Guid.NewGuid().ToString("N").Substring(0, 8);
            dto.Estado = "ACTIVO";

            _context.Productos.Add(dto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByCode), new { codigo = dto.Codigo }, dto);
        }

        // PUT api/producto/{codigo}
        [HttpPut("{codigo}")]
        public async Task<IActionResult> Update(string codigo, Producto dto)
        {
            var prod = await _context.Productos
                .FirstOrDefaultAsync(p => p.Codigo == codigo);

            if (prod == null) return NotFound();

            prod.Nombre = dto.Nombre;
            prod.Categoria = dto.Categoria;
            prod.CostoProduccion = dto.CostoProduccion;
            prod.StockActual = dto.StockActual;
            prod.StockMinimo = dto.StockMinimo;
            prod.ImagenUrl = dto.ImagenUrl;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE (borrado lógico) api/producto/{codigo}
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(string codigo)
        {
            var prod = await _context.Productos
                .FirstOrDefaultAsync(p => p.Codigo == codigo);

            if (prod == null) return NotFound();

            prod.Estado = "BORRADO";

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

