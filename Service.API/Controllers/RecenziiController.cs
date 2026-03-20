using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.API.Data;
using Service.API.Models;

namespace Service.API.Controllers
{
    // DTO pentru creare
    public record RecenzieCreateDto(
        Guid ServiciuId,
        [System.ComponentModel.DataAnnotations.Required,
         System.ComponentModel.DataAnnotations.MaxLength(800)]
        string Text,
        [System.ComponentModel.DataAnnotations.Range(1,5)]
        int Rating
    );

    [Route("api/[controller]")]
    [ApiController]
    public class RecenziiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RecenziiController(AppDbContext context) => _context = context;

        // GET: api/Recenzii  (lista)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recenzie>>> GetRecenzii()
        {
            return await _context.Recenzii
                .AsNoTracking()
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        // GET: api/Recenzii/{id}  (un element)
        [HttpGet("{id}")]
        public async Task<ActionResult<Recenzie>> GetRecenzie(Guid id)
        {
            var rec = await _context.Recenzii.FindAsync(id);
            if (rec is null) return NotFound();
            return rec;
        }

        // PUT: api/Recenzii/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecenzie(Guid id, Recenzie recenzie)
        {
            if (id != recenzie.Id) return BadRequest();

            _context.Entry(recenzie).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Recenzii.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        // POST: api/Recenzii
        [HttpPost]
        public async Task<ActionResult<Recenzie>> PostRecenzie([FromBody] RecenzieCreateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var recenzie = new Recenzie
            {
                Id = Guid.NewGuid(),
                ServiciuId = dto.ServiciuId,
                Text = dto.Text.Trim(),
                // Rating în model e byte → conversie explicită
                Rating = (byte)dto.Rating,
                CreatedAt = DateTime.UtcNow
            };

            _context.Recenzii.Add(recenzie);
            await _context.SaveChangesAsync();

            // acum există GetRecenzie(Guid id)
            return CreatedAtAction(nameof(GetRecenzie), new { id = recenzie.Id }, recenzie);
        }

        // DELETE: api/Recenzii/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecenzie(Guid id)
        {
            var rec = await _context.Recenzii.FindAsync(id);
            if (rec is null) return NotFound();

            _context.Recenzii.Remove(rec);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
