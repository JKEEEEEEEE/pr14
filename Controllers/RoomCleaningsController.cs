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
    public class RoomCleaningsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public RoomCleaningsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/RoomCleanings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomCleaning>>> GetRoomCleanings()
        {
          if (_context.RoomCleanings == null)
          {
              return NotFound();
          }
            return await _context.RoomCleanings.ToListAsync();
        }

        // GET: api/RoomCleanings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomCleaning>> GetRoomCleaning(int id)
        {
          if (_context.RoomCleanings == null)
          {
              return NotFound();
          }
            var roomCleaning = await _context.RoomCleanings.FindAsync(id);

            if (roomCleaning == null)
            {
                return NotFound();
            }

            return roomCleaning;
        }

        // PUT: api/RoomCleanings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomCleaning(int id, RoomCleaning roomCleaning)
        {
            if (id != roomCleaning.IdRoomCleaning)
            {
                return BadRequest();
            }

            _context.Entry(roomCleaning).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomCleaningExists(id))
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

        // POST: api/RoomCleanings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomCleaning>> PostRoomCleaning(RoomCleaning roomCleaning)
        {
          if (_context.RoomCleanings == null)
          {
              return Problem("Entity set 'HotelsContext.RoomCleanings'  is null.");
          }
            _context.RoomCleanings.Add(roomCleaning);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomCleaning", new { id = roomCleaning.IdRoomCleaning }, roomCleaning);
        }

        // DELETE: api/RoomCleanings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomCleaning(int id)
        {
            if (_context.RoomCleanings == null)
            {
                return NotFound();
            }
            var roomCleaning = await _context.RoomCleanings.FindAsync(id);
            if (roomCleaning == null)
            {
                return NotFound();
            }

            _context.RoomCleanings.Remove(roomCleaning);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("LogicDel")]
        public IActionResult LogicDeleteAnimal(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.RoomCleanings.FirstOrDefault(t => t.IdRoomCleaning == id);
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
                var user = _context.RoomCleanings.FirstOrDefault(t => t.IdRoomCleaning == id);
                if (user != null)
                {
                    user.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("paged-logs")]
        public async Task<ActionResult<List<List<RoomCleaning>>>> GetPagedAnimalLogs(int Pages, int Entities)
        {
            var totalLogsCount = await _context.RoomCleanings.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalLogsCount / Entities);

            var answer = new List<List<RoomCleaning>>();

            var currentId = 1;
            while (true)
            {
                var logs = await _context.RoomCleanings
                    .Where(log => log.IdRoomCleaning >= currentId)
                    .OrderBy(log => log.IdRoomCleaning)
                    .Take(Entities)
                    .ToListAsync();

                if (!logs.Any())
                {
                    break;
                }

                var pageLogs = new List<RoomCleaning>(logs);
                answer.Add(pageLogs);

                if (answer.Count == Pages || logs.Count < Entities)
                {
                    break;
                }

                currentId = logs.Last().IdRoomCleaning + 1;
            }

            return answer;
        }

        private bool RoomCleaningExists(int id)
        {
            return (_context.RoomCleanings?.Any(e => e.IdRoomCleaning == id)).GetValueOrDefault();
        }
    }
}
