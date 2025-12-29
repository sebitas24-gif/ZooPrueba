using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Serilog;

namespace ZooPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspeciesController : ControllerBase
    {
        private readonly ZooPruebaContext _context;

        public EspeciesController(ZooPruebaContext context)
        {
            _context = context;
        }

        // GET: api/Especies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especie>>> GetEspecie()
        {
            return await _context.Especies.ToListAsync();
        }

        // GET: api/Especies/5
        [HttpGet("Codigo/{id}")]
        public async Task<ActionResult<Especie>> GetEspecie(int id)
        {
            var especie = await _context.Especies.FindAsync(id);

            if (especie == null)
            {
                return NotFound();
            }

            return especie;
        }

        // PUT: api/Especies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecie(int id, Especie especie)
        {
            if (id != especie.Codigo)
            {
                Log.Error("No coinciden los IDs al actualizar la especie con ID {EspecieId}.", id);
                return BadRequest();
            }

            _context.Entry(especie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecieExists(id))
                {
                    Log.Error("Especie con ID {EspecieId} no encontrada al intentar actualizar.", id);
                    return NotFound();
                }
                else
                {
                    Log.Error("Error de concurrencia al actualizar la especie con ID {EspecieId}.", id);
                    throw;
                }
            }
            Log.Information("Especie con ID {EspecieId} actualizada correctamente.", id);
            return NoContent();
        }

        // POST: api/Especies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Especie>> PostEspecie(Especie especie)
        {
            _context.Especies.Add(especie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEspecie", new { id = especie.Codigo }, especie);
        }

        // DELETE: api/Especies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecie(int id)
        {
            var especie = await _context.Especies.FindAsync(id);
            if (especie == null)
            {
                return NotFound();
            }

            _context.Especies.Remove(especie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspecieExists(int id)
        {
            return _context.Especies.Any(e => e.Codigo == id);
        }
    }
}
