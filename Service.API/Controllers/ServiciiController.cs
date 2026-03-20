using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.API.Data;
using Service.API.Models; //entitatea Serviciu

namespace Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiciiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Servicii
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Serviciu>>> GetServicii()
        {
            return await _context.Servicii.ToListAsync();
        }

        // GET: api/Servicii
        [HttpGet("{id}")]
        public async Task<ActionResult<Serviciu>> GetServiciu(Guid id)
        {
            var serviciu = await _context.Servicii.FindAsync(id);

            if (serviciu == null)
            {
                return NotFound();
            }

            return serviciu;
        }

        // PUT: api/Servicii
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiciu(Guid id, Serviciu serviciu)
        {
            if (id != serviciu.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviciu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiciuExists(id))
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

        // POST: api/Servicii
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Serviciu>> PostServiciu(Serviciu serviciu)
        {
            _context.Servicii.Add(serviciu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiciu", new { id = serviciu.Id }, serviciu);
        }

        // DELETE: api/Servicii
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiciu(Guid id)
        {
            var serviciu = await _context.Servicii.FindAsync(id);
            if (serviciu == null)
            {
                return NotFound();
            }

            _context.Servicii.Remove(serviciu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiciuExists(Guid id)
        {
            return _context.Servicii.Any(e => e.Id == id);
        }
    }
}
