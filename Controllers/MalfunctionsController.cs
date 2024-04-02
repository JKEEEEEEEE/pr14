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
    public class MalfunctionsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public MalfunctionsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/Malfunctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Malfunction>>> GetMalfunctions()
        {
          if (_context.Malfunctions == null)
          {
              return NotFound();
          }
            return await _context.Malfunctions.ToListAsync();
        }

        // GET: api/Malfunctions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Malfunction>> GetMalfunction(int id)
        {
          if (_context.Malfunctions == null)
          {
              return NotFound();
          }
            var malfunction = await _context.Malfunctions.FindAsync(id);

            if (malfunction == null)
            {
                return NotFound();
            }

            return malfunction;
        }

        // PUT: api/Malfunctions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMalfunction(int id, Malfunction malfunction)
        {
            if (id != malfunction.IdMalfunctions)
            {
                return BadRequest();
            }

            _context.Entry(malfunction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MalfunctionExists(id))
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

        // POST: api/Malfunctions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Malfunction>> PostMalfunction(Malfunction malfunction)
        {
          if (_context.Malfunctions == null)
          {
              return Problem("Entity set 'HotelsContext.Malfunctions'  is null.");
          }
            _context.Malfunctions.Add(malfunction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMalfunction", new { id = malfunction.IdMalfunctions }, malfunction);
        }

        // DELETE: api/Malfunctions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMalfunction(int id)
        {
            if (_context.Malfunctions == null)
            {
                return NotFound();
            }
            var malfunction = await _context.Malfunctions.FindAsync(id);
            if (malfunction == null)
            {
                return NotFound();
            }

            _context.Malfunctions.Remove(malfunction);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("LogicDel")]
        public IActionResult LogicDeleteAnimal(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.Malfunctions.FirstOrDefault(t => t.IdMalfunctions == id);
                if (user != null)
                {
                    user.IsDeleted = true;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("LogicRest")]
        public IActionResult LogicRestoreAnimal(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.Malfunctions.FirstOrDefault(t => t.IdMalfunctions == id);
                if (user != null)
                {
                    user.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("paged-logs")]
        public async Task<ActionResult<List<List<Malfunction>>>> GetPagedAnimalLogs(int Pages, int Entities)
        {
            var totalLogsCount = await _context.Malfunctions.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalLogsCount / Entities);

            var answer = new List<List<Malfunction>>();

            var currentId = 1;
            while (true)
            {
                var logs = await _context.Malfunctions
                    .Where(log => log.IdMalfunctions >= currentId)
                    .OrderBy(log => log.IdMalfunctions)
                    .Take(Entities)
                    .ToListAsync();

                if (!logs.Any())
                {
                    break;
                }

                var pageLogs = new List<Malfunction>(logs);
                answer.Add(pageLogs);

                if (answer.Count == Pages || logs.Count < Entities)
                {
                    break;
                }

                currentId = logs.Last().IdMalfunctions + 1;
            }

            return answer;
        }

        private bool MalfunctionExists(int id)
        {
            return (_context.Malfunctions?.Any(e => e.IdMalfunctions == id)).GetValueOrDefault();
        }
    }
}
