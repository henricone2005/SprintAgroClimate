using AgroClimate.Data;
using AgroClimate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroClimate.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FazendaControllers(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fazenda>>> GetFazendas()
    {
        return await _context.Fazendas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Fazenda>> GetFazenda(int id)
    {
        var fazenda = await _context.Fazendas.FindAsync(id);

        if (fazenda == null)
        {
            return NotFound();
        }

        return fazenda;
    }

    [HttpPost]
    public async Task<ActionResult<Fazenda>> PostFazenda(Fazenda fazenda)
    {
        _context.Fazendas.Add(fazenda);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFazenda), new { id = fazenda.Id }, fazenda);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFazenda(int id, Fazenda fazenda)
    {
        if (id != fazenda.Id)
        {
            return BadRequest();
        }

        _context.Entry(fazenda).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFazenda(int id)
    {
        var fazenda = await _context.Fazendas.FindAsync(id);
        if (fazenda == null)
        {
            return NotFound();
        }

        _context.Fazendas.Remove(fazenda);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
