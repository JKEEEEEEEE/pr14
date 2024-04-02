using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotels.Models;

namespace Hotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorHistsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public AdministratorHistsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/AdministratorHists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministratorHist>>> GetAdministratorHists()
        {
          if (_context.AdministratorHists == null)
          {
              return NotFound();
          }
            return await _context.AdministratorHists.ToListAsync();
        }

        // GET: api/AdministratorHists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdministratorHist>> GetAdministratorHist(int id)
        {
          if (_context.AdministratorHists == null)
          {
              return NotFound();
          }
            var administratorHist = await _context.AdministratorHists.FindAsync(id);

            if (administratorHist == null)
            {
                return NotFound();
            }

            return administratorHist;
        }

        // PUT: api/AdministratorHists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministratorHist(int id, AdministratorHist administratorHist)
        {
            if (id != administratorHist.IdAdministratorHist)
            {
                return BadRequest();
            }

            _context.Entry(administratorHist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorHistExists(id))
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

        // POST: api/AdministratorHists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdministratorHist>> PostAdministratorHist(AdministratorHist administratorHist)
        {
          if (_context.AdministratorHists == null)
          {
              return Problem("Entity set 'HotelsContext.AdministratorHists'  is null.");
          }
            _context.AdministratorHists.Add(administratorHist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministratorHist", new { id = administratorHist.IdAdministratorHist }, administratorHist);
        }

        // DELETE: api/AdministratorHists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministratorHist(int id)
        {
            if (_context.AdministratorHists == null)
            {
                return NotFound();
            }
            var administratorHist = await _context.AdministratorHists.FindAsync(id);
            if (administratorHist == null)
            {
                return NotFound();
            }

            _context.AdministratorHists.Remove(administratorHist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdministratorHistExists(int id)
        {
            return (_context.AdministratorHists?.Any(e => e.IdAdministratorHist == id)).GetValueOrDefault();
        }
    }
}
