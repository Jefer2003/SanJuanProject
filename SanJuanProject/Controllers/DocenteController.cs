using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanJuanProject.DTO;
using SanJuanProject.Models;
using static SanJuanProject.Data.AppData;

namespace SanJuanProject.Controllers
{
    [ApiController]
    [Route("api/docentes")]
    public class DocentesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocentesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/docentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocenteResponseDto>>> GetDocentes()
        {
            var docentes = await _context.docente
                .Select(d => new DocenteResponseDto
                {
                    Id = d.Id,
                    Apellidos = d.Apellidos,
                    Nombres = d.Nombres,
                    Profesion = d.Profesion,
                    FechaNacimiento = d.FechaNacimiento,
                    Correo = d.Correo
                })
                .ToListAsync();

            return Ok(docentes);
        }

        // GET: /api/docentes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DocenteResponseDto>> GetDocente(int id)
        {
            var docente = await _context.docente.FindAsync(id);
            if (docente == null)
                return NotFound();

            var dto = new DocenteResponseDto
            {
                Id = docente.Id,
                Apellidos = docente.Apellidos,
                Nombres = docente.Nombres,
                Profesion = docente.Profesion,
                FechaNacimiento = docente.FechaNacimiento,
                Correo = docente.Correo
            };

            return Ok(dto);
        }

        // POST: /api/docentes
        [HttpPost]
        public async Task<ActionResult> PostDocente(DocenteRequestDto dto)
        {
            var docente = new Docente
            {
                Apellidos = dto.Apellidos,
                Nombres = dto.Nombres,
                Profesion = dto.Profesion,
                FechaNacimiento = dto.FechaNacimiento,
                Correo = dto.Correo
            };

            _context.docente.Add(docente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocente), new { id = docente.Id }, docente);
        }

        // PUT: /api/docentes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocente(int id, DocenteRequestDto dto)
        {
            var docente = await _context.docente.FindAsync(id);
            if (docente == null)
                return NotFound();

            docente.Apellidos = dto.Apellidos;
            docente.Nombres = dto.Nombres;
            docente.Profesion = dto.Profesion;
            docente.FechaNacimiento = dto.FechaNacimiento;
            docente.Correo = dto.Correo;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/docentes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocente(int id)
        {
            var docente = await _context.docente.FindAsync(id);
            if (docente == null)
                return NotFound();

            _context.docente.Remove(docente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
