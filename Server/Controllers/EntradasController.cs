using _2doParcial_AP1RonellIntento.Server;
using _2doParcial_AP1RonellIntento.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2doParcial_AP1RonellIntento.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntradasController : ControllerBase
{
    private readonly Context _context;

    public EntradasController(Context context)
    {
        _context = context;
    }

    // GET: api/Entradas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Entradas>>> GetEntradas()
    {
        if (_context.Entradas == null)
        {
            return NotFound();
        }
        return await _context.Entradas.ToListAsync();
    }

    // GET: api/Entradas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Entradas>> GetEntradas(int id)
    {
        if (_context.Entradas == null)
        {
            return NotFound();
        }
        var entradas = await _context.Entradas
                    .Include(e => e.EntradasDetalles)
                    .Where(e => e.EntradaId == id)
                    .FirstOrDefaultAsync();

        if (entradas == null)
        {
            return NotFound();
        }

        return entradas;
    }

    // PUT: api/Entradas/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEntradas(int id, Entradas entradas)
    {
        if (id != entradas.EntradaId)
        {
            return BadRequest();
        }

        _context.Entry(entradas).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EntradasExists(id))
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

    // POST: api/Entradas
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Entradas>> PostEntradas(Entradas entradas)
    {
        if (!EntradasExists(entradas.EntradaId))
        {
            _context.Entradas.Add(entradas);
        }
        else
        {
            _context.Entradas.Update(entradas);
        }

        await _context.SaveChangesAsync();

        return Ok(entradas);
    }

    // DELETE: api/Entradas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEntradas(int id)
    {
        if (_context.Entradas == null)
        {
            return NotFound();
        }
        var entradas = await _context.Entradas.FindAsync(id);
        if (entradas == null)
        {
            return NotFound();
        }

        _context.Entradas.Remove(entradas);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EntradasExists(int id)
    {
        return (_context.Entradas?.Any(e => e.EntradaId == id)).GetValueOrDefault();
    }
}
