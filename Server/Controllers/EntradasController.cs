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
                    .Include(e => e.EntradasDetalle)
                    .Where(e => e.EntradaId == id)
                    .FirstOrDefaultAsync();

        if (entradas == null)
        {
            return NotFound("Esta entrada no existe");
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
    [HttpPost("{id}")]
    public async Task<ActionResult<Entradas>> PostEntradas(int id, Entradas entradas)
    {
        if (!EntradasExists(entradas.EntradaId))
        {
            Productos? producto = new Productos();

            var productoBase = await _context.Productos.FindAsync(entradas.ProductoId);

            if (entradas.ProductoId == 0 || entradas.CantidadProducida <= 0 || entradas.EntradasDetalle.Count() <= 0)
            {
                return BadRequest("Debe de rellenar todos los campos");
            }

            foreach (var productoUtilizado in entradas.EntradasDetalle)
            {
                producto = _context.Productos.Find(productoUtilizado.ProductoId);

                int pruebaProducto = 0;
                pruebaProducto = producto.Existencia - productoUtilizado.CantidadUtilizada;

                if(pruebaProducto < 0)
                {
                    return NotFound($"No hay {producto.Descripcion} en el almacen");
                }

                if(entradas.EntradasDetalle.Count() > 3 || entradas.EntradasDetalle.Count < 3)
                {
                    return BadRequest();
                }

                //var productoBase = await _context.Productos.FindAsync(entradas.ProductoId);

                if (productoBase != null)
                {
                    /*if(productoBase.ProductoId == 5 || productoBase.ProductoId == 7)
                    {

                    }
                    //productoBase.Existencia = 0;
                    productoBase.Existencia += entradas.CantidadProducida;
                    _context.Productos.Update(productoBase);*/
                }
                
                    if (producto != null)
                    {
                        producto.Existencia -= productoUtilizado.CantidadUtilizada;
                        
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
                    }
            }
            productoBase.Existencia += entradas.CantidadProducida;
            _context.Productos.Update(productoBase);
            _context.Entradas.Add(entradas);
        }
        else
        {
            var entradaAnterior = _context.Entradas
                             .Include(e => e.EntradasDetalle)
                             .AsNoTracking()
                             .FirstOrDefault(e => e.EntradaId == entradas.EntradaId);

            Productos? producto = new Productos();

            //EntradasDetalle detalle = new EntradasDetalle();

            if (entradaAnterior != null && entradaAnterior.EntradasDetalle != null)
            {
                foreach (var productoUtilizado in entradas.EntradasDetalle)
                {
                    producto = _context.Productos.Find(productoUtilizado.ProductoId);

                    var detalle = entradaAnterior.EntradasDetalle.FirstOrDefault(d => d.DetalleId == productoUtilizado.DetalleId);

                    if (entradas.EntradasDetalle.Count() > 3 || entradas.EntradasDetalle.Count < 3)
                    {
                        return BadRequest();
                    }
                    if (detalle != null)
                    {
                        _context.Entry(productoUtilizado).State = EntityState.Modified;
                    }
                }

                foreach (var productoUtilizadoD in entradas.EntradasDetalle)
                {
                    var detalle = entradaAnterior.EntradasDetalle.FirstOrDefault(d => d.DetalleId == productoUtilizadoD.DetalleId);

                    if(detalle == null)
                    {
                        producto.Existencia -= productoUtilizadoD.CantidadUtilizada;
                        _context.Entry(productoUtilizadoD).State = EntityState.Modified;
                        _context.Productos.Update(producto);
                        _context.EntradasDetalles.Add(productoUtilizadoD);
                    }
                }   
                    /*else
                    {
                        int pruebaProducto = 0;
                        pruebaProducto = producto.Existencia - productoUtilizado.CantidadUtilizada;

                        if (pruebaProducto < 0)
                        {
                            return NotFound($"No hay {producto.Descripcion} en el almacen");
                        }

                        foreach (var productoproducido in entradas.EntradasDetalle)
                        {
                            if (productoUtilizado != null)
                            {
                                producto = _context.Productos.Find(productoproducido.ProductoId);

                                if (productoproducido != null)
                                {
                                    if (producto != null)
                                    {
                                        producto.Existencia -= productoUtilizado.CantidadUtilizada;
                                        producto.Existencia += entradas.CantidadProducida;
                                        _context.Productos.Update(producto);
                                        await _context.SaveChangesAsync();
                                        _context.Entry(producto).State = EntityState.Detached;
                                    }
                                }
                            }
                        }*/

                }

            /*foreach (var productoUtilizado in entradas.EntradasDetalle)
            {
                if (!productoUtilizado.Equals(entradas.EntradasDetalle.Where(p => p.DetalleId == productoUtilizado.DetalleId)))
                {
                    foreach (var productoproducido in entradas.EntradasDetalle)
                    {
                        if (productoproducido != null)
                        {
                            producto = _context.Productos.Find(productoproducido.ProductoId);

                            if (productoproducido != null)
                            {
                                if (producto != null)
                                {
                                    //producto.Existencia -= productoproducido.CantidadUtilizada;
                                    //producto.Existencia += entradas.CantidadProducida;
                                    _context.Productos.Update(producto);
                                    await _context.SaveChangesAsync();
                                    _context.Entry(producto).State = EntityState.Detached;
                                }
                            }
                        }
                    }
                }
            }*/

            _context.Entry(entradas).State = EntityState.Modified;
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

        var entradas = await _context.Entradas
                            .Include(e => e.EntradasDetalle)
                            .FirstOrDefaultAsync(e => e.EntradaId == id);

        if (entradas == null)
        {
            return NotFound();
        }
        var productoBase = await _context.Productos.FindAsync(entradas.ProductoId);

        foreach (var productoUtilizado in entradas.EntradasDetalle)
        {
            var producto = await _context.Productos.FindAsync(productoUtilizado.ProductoId);

            if (producto != null)
            {
                producto.Existencia += productoUtilizado.CantidadUtilizada;
                _context.Productos.Update(producto);
            }
        }


        if (productoBase != null)
        {
            productoBase.Existencia -= entradas.CantidadProducida;
            _context.Productos.Update(productoBase);
        }

        _context.Entradas.Remove(entradas);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("EliminarDetalle/{detalleId}")]
    public async Task<IActionResult> DeleteDetalles(int detalleId)
    {

        if (detalleId < 0)
        {
            return BadRequest();
        }
        else
        {
            if (DetalleExists(detalleId))
            {
                var entradas = await _context.EntradasDetalles.FirstOrDefaultAsync(e => e.DetalleId == detalleId);
                
                    var producto = await _context.Productos.FindAsync(entradas.ProductoId);

                    if (entradas is null)
                    {
                            return NotFound();
                    }
                
                _context.EntradasDetalles.Remove(entradas);
                producto.Existencia += entradas.CantidadUtilizada;
            }

            else
            {
                var entradas = await _context.EntradasDetalles.FirstOrDefaultAsync(e => e.DetalleId == detalleId);
                if (entradas is null)
                {
                    return NotFound();
                }

                _context.EntradasDetalles.Remove(entradas);
                //producto.Existencia += productoUtilizado.CantidadUtilizada;
            }
            await _context.SaveChangesAsync();
        }
        return Ok(detalleId);
    }

    private bool EntradasExists(int id)
    {
        return (_context.Entradas?.Any(e => e.EntradaId == id)).GetValueOrDefault();
    }

    private bool DetalleExists(int detalleId)
    {
        return (_context.EntradasDetalles?.Any(ed => ed.DetalleId == detalleId)).GetValueOrDefault();
    }

    private bool CantidadUExits(int cantidadU)
    {
        return (_context.EntradasDetalles?.Any(c => c.CantidadUtilizada == cantidadU)).GetValueOrDefault();
    }
}
