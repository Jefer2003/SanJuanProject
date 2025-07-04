using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanJuanProject.DTO;
using SanJuanProject.Models;
using static SanJuanProject.Data.AppData;

namespace SanJuanProject.Controllers
{
    [ApiController]
    [Route("api/cursos")]
    public class CursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoResponseDto>>> GetCursos()
        {
            var cursos = await _context.curso
                .Include(c => c.Docente)
                .Select(c => new CursoResponseDto
                {
                    Id = c.Id,
                    NombreCurso = c.NombreCurso,
                    Creditos = c.Creditos,
                    HorasSemanal = c.HorasSemanal,
                    Ciclo = c.Ciclo,
                    IdDocente = c.IdDocente,
                    NombreDocente = c.Docente.Nombres + " " + c.Docente.Apellidos
                })
                .ToListAsync();

            return Ok(cursos);
        }

        // GET: /api/cursos/ciclo/{ciclo}
        [HttpGet("ciclo/{ciclo}")]
        public async Task<ActionResult<IEnumerable<CursoResponseDto>>> GetCursosPorCiclo(string ciclo)
        {
            var cursos = await _context.curso
                .Include(c => c.Docente)
                .Where(c => c.Ciclo == ciclo)
                .Select(c => new CursoResponseDto
                {
                    Id = c.Id,
                    NombreCurso = c.NombreCurso,
                    Creditos = c.Creditos,
                    HorasSemanal = c.HorasSemanal,
                    Ciclo = c.Ciclo,
                    IdDocente = c.IdDocente,
                    NombreDocente = c.Docente.Nombres + " " + c.Docente.Apellidos
                })
                .ToListAsync();

            if (!cursos.Any())
                return NotFound();

            return Ok(cursos);
        }

        // POST: /api/cursos
        [HttpPost]
        public async Task<ActionResult> PostCurso(CursoRequestDto dto)
        {
            if (!await _context.docente.AnyAsync(d => d.Id == dto.IdDocente))
                return BadRequest("Docente no encontrado.");

            var curso = new Curso
            {
                NombreCurso = dto.NombreCurso,
                Creditos = dto.Creditos,
                HorasSemanal = dto.HorasSemanal,
                Ciclo = dto.Ciclo,
                IdDocente = dto.IdDocente
            };

            _context.curso.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCursos), new { id = curso.Id }, curso);
        }

        // PUT: /api/cursos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, CursoRequestDto dto)
        {
            var curso = await _context.curso.FindAsync(id);
            if (curso == null)
                return NotFound();

            if (!await _context.docente.AnyAsync(d => d.Id == dto.IdDocente))
                return BadRequest("Docente no encontrado.");

            curso.NombreCurso = dto.NombreCurso;
            curso.Creditos = dto.Creditos;
            curso.HorasSemanal = dto.HorasSemanal;
            curso.Ciclo = dto.Ciclo;
            curso.IdDocente = dto.IdDocente;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/cursos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.curso.FindAsync(id);
            if (curso == null)
                return NotFound();

            _context.curso.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
