using AgroClimate.Data;
using AgroClimate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroClimate.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FazendaControllers : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FazendaControllers(ApplicationDbContext context)
    {
        _context = context;
    }

    public class CreateFazendaDto
    {
        public string Nome { get; set; } = string.Empty;
        public double Area { get; set; } // Altera de string para double
    }

    public class UpdateFazendaDto
    {
        public string Nome { get; set; } = string.Empty; 
        public double Area { get; set; } // Altera de string para double
    }

    [HttpPost]
    public async Task<ActionResult<Fazenda>> CreateFazenda(CreateFazendaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var fazenda = new Fazenda
        {
            Nome = dto.Nome,
            Area = dto.Area,
        };

        _context.Fazendas.Add(fazenda);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFazendaById), new { id = fazenda.Id }, fazenda);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fazenda>>> GetFazendas()
    {
        return await _context.Fazendas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Fazenda>> GetFazendaById(int id)
    {
        var fazenda = await _context.Fazendas
            .Include(p => p.Agricultores) // Inclui os agricultores associados
            .FirstOrDefaultAsync(p => p.Id == id);

        if (fazenda == null)
        {
            return NotFound($"Fazenda com ID {id} não encontrado.");
        }

        return Ok(fazenda); // Retorna Ok com o objeto
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFazenda(int id, UpdateFazendaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var fazenda = await _context.Fazendas.FindAsync(id);
        if (fazenda == null)
        {
            return NotFound($"Fazenda com ID {id} não encontrado.");
        }

        fazenda.Nome = dto.Nome;
        fazenda.Area = dto.Area;

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
            return NotFound($"Fazenda com ID {id} não encontrado.");
        }

        _context.Fazendas.Remove(fazenda);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
