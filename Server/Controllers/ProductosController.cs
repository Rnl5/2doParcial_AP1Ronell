using _2doParcial_AP1RonellIntento.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2doParcial_AP1RonellIntento.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly Context _context;

    public ProductosController(Context context)
    {
        _context = context;
    }

    // GET: api/Productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
    {
        if (_context.Productos == null)
        {
            return NotFound();
        }
        return await _context.Productos.ToListAsync();
    }

    // GET: api/Productos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Productos>> GetProductos(int id)
    {
        if (_context.Productos == null)
        {
            return NotFound();
        }
        var productos = await _context.Productos
                    .Where(p => p.ProductoId == id)
                    .FirstOrDefaultAsync();

        if (productos == null)
        {
            return NotFound();
        }

        return productos;
    }

    // PUT: api/Productos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductos(int id, Productos productos)
    {
        if (id != productos.ProductoId)
        {
            return BadRequest();
        }

        _context.Entry(productos).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductosExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Productos
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Productos>> PostProductos(Productos productos)
    {
        if (!ProductosExists(productos.ProductoId))
        {
            _context.Productos.Add(productos);
        }
        else
        {
            _context.Productos.Update(productos);
        }

        await _context.SaveChangesAsync();

        return Ok(productos);
    }

    // DELETE: api/Productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductos(int id)
    {
        if (_context.Productos == null)
        {
            return NotFound();
        }
        var productos = await _context.Productos.FindAsync(id);
        if (productos == null)
        {
            return NotFound();
        }

        _context.Productos.Remove(productos);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductosExists(int id)
    {
        return (_context.Productos?.Any(p => p.ProductoId == id)).GetValueOrDefault();
    }
}

