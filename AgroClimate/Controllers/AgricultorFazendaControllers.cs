using AgroClimate.Data;
using AgroClimate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroClimate.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgricultorFazendaControllers(ApplicationDbContext context) : ControllerBase
{
private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgricultorFazenda>>> GetAgricultorFazendas()
    {
        return await _context.AgricultorFazendas
                             .Include(af => af.Agricultor)
                             .Include(af => af.Fazenda)
                             .ToListAsync();
    }

    [HttpGet("{agricultorId}/{fazendaId}")]
    public async Task<ActionResult<AgricultorFazenda>> GetAgricultorFazenda(int agricultorId, int fazendaId)
    {
        var agricultorFazenda = await _context.AgricultorFazendas
                                              .Include(af => af.Agricultor)
                                              .Include(af => af.Fazenda)
                                              .FirstOrDefaultAsync(af => af.AgricultorId == agricultorId && af.FazendaId == fazendaId);

        if (agricultorFazenda == null)
        {
            return NotFound();
        }

        return agricultorFazenda;
    }

    [HttpPost]
    public async Task<ActionResult<AgricultorFazenda>> PostAgricultorFazenda(AgricultorFazenda agricultorFazenda)
    {
        _context.AgricultorFazendas.Add(agricultorFazenda);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAgricultorFazenda), new { agricultorId = agricultorFazenda.AgricultorId, fazendaId = agricultorFazenda.FazendaId }, agricultorFazenda);
    }

    [HttpDelete("{agricultorId}/{fazendaId}")]
    public async Task<IActionResult> DeleteAgricultorFazenda(int agricultorId, int fazendaId)
    {
        var agricultorFazenda = await _context.AgricultorFazendas
                                              .FirstOrDefaultAsync(af => af.AgricultorId == agricultorId && af.FazendaId == fazendaId);
        if (agricultorFazenda == null)
        {
            return NotFound();
        }

        _context.AgricultorFazendas.Remove(agricultorFazenda);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
