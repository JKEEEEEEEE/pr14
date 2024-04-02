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
    public class FloorsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public FloorsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/Floors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Floor>>> GetFloors()
        {
          if (_context.Floors == null)
          {
              return NotFound();
          }
            return await _context.Floors.ToListAsync();
        }

        // GET: api/Floors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Floor>> GetFloor(int id)
        {
          if (_context.Floors == null)
          {
              return NotFound();
          }
            var floor = await _context.Floors.FindAsync(id);

            if (floor == null)
            {
                return NotFound();
            }

            return floor;
        }

        // PUT: api/Floors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFloor(int id, Floor floor)
        {
            if (id != floor.IdFloor)
            {
                return BadRequest();
            }

            _context.Entry(floor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloorExists(id))
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

        // POST: api/Floors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Floor>> PostFloor(Floor floor)
        {
          if (_context.Floors == null)
          {
              return Problem("Entity set 'HotelsContext.Floors'  is null.");
          }
            _context.Floors.Add(floor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFloor", new { id = floor.IdFloor }, floor);
        }

        // DELETE: api/Floors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFloor(int id)
        {
            if (_context.Floors == null)
            {
                return NotFound();
            }
            var floor = await _context.Floors.FindAsync(id);
            if (floor == null)
            {
                return NotFound();
            }

            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("LogicDel")]
        public IActionResult LogicDeleteAnimal(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.Floors.FirstOrDefault(t => t.IdFloor == id);
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
                var user = _context.Floors.FirstOrDefault(t => t.IdFloor == id);
                if (user != null)
                {
                    user.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("paged-logs")]
        public async Task<ActionResult<List<List<Floor>>>> GetPagedAnimalLogs(int Pages, int Entities)
        {
            var totalLogsCount = await _context.Floors.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalLogsCount / Entities);

            var answer = new List<List<Floor>>();

            var currentId = 1;
            while (true)
            {
                var logs = await _context.Floors
                    .Where(log => log.IdFloor >= currentId)
                    .OrderBy(log => log.IdFloor)
                    .Take(Entities)
                    .ToListAsync();

                if (!logs.Any())
                {
                    break;
                }

                var pageLogs = new List<Floor>(logs);
                answer.Add(pageLogs);

                if (answer.Count == Pages || logs.Count < Entities)
                {
                    break;
                }

                currentId = logs.Last().IdFloor + 1;
            }

            return answer;
        }

        private bool FloorExists(int id)
        {
            return (_context.Floors?.Any(e => e.IdFloor == id)).GetValueOrDefault();
        }
    }
}
