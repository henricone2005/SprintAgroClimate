using AgroClimate.Data;
using AgroClimate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroClimate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgricultorControllers : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AgricultorControllers(ApplicationDbContext context)
        {
            _context = context;
        }

        public class CreateAgricultorDto
        {
            public string Nome { get; set; } = string.Empty;
            public string CPF { get; set; } = string.Empty;
            public List<int> FazendaIds { get; set; } = new List<int>();
        }

        public class UpdateAgricultorDto
        {
            public string Nome { get; set; } = string.Empty;
            public string CPF { get; set; } = string.Empty;
        }

        public class AgricultorDto
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string CPF { get; set; } = string.Empty;
            public List<int> FazendaIds { get; set; } = new List<int>();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAgricultor([FromBody] CreateAgricultorDto dto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Declare a variável agricultor antes de usá-la
    var agricultor = new Agricultor
    {
        Nome = dto.Nome,
        Cpf = dto.CPF,
    };
            if (dto.FazendaIds != null && dto.FazendaIds.Any())
            {
                var fazendas = await _context.FazendasSP
                    .Where(p => dto.FazendaIds.Contains(p.Id))
                    .ToListAsync();

                if (fazendas.Count != dto.FazendaIds.Count)
                {
                    return BadRequest("Um ou mais IDs de fazenda são inválidos.");
                }

                foreach (var fazenda in fazendas)
                {
                    agricultor.Fazendas.Add(fazenda);
                }
            }

            _context.AgricultoresSP.Add(agricultor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAgricultorById), new { id = agricultor.Id }, agricultor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgricultorDto>>> GetAgricultores()
        {
            var agricultores = await _context.AgricultoresSP
                .Select(p => new AgricultorDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    CPF = p.Cpf,
                    FazendaIds = p.Fazendas.Select(fazenda => fazenda.Id).ToList()
                })
                .ToListAsync();

            return Ok(agricultores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgricultorDto>> GetAgricultorById(int id)
        {
            var agricultor = await _context.AgricultoresSP
                .Include(p => p.Fazendas)
                .Select(p => new AgricultorDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    CPF = p.Cpf,
                    FazendaIds = p.Fazendas.Select(fazenda => fazenda.Id).ToList()
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (agricultor == null)
            {
                return NotFound($"Agricultor com ID {id} não encontrado.");
            }

            return Ok(agricultor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgricultor(int id, UpdateAgricultorDto dto)
        {
            var agricultor = await _context.AgricultoresSP.FindAsync(id);
            if (agricultor == null)
            {
                return NotFound($"Agricultor com ID {id} não encontrado.");
            }

            agricultor.Nome = dto.Nome;
            agricultor.Cpf = dto.CPF;

            _context.Entry(agricultor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgricultor(int id)
        {
            var agricultor = await _context.AgricultoresSP.FindAsync(id);
            if (agricultor == null)
            {
                return NotFound($"Agricultor com ID {id} não encontrado.");
            }

            _context.AgricultoresSP.Remove(agricultor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
