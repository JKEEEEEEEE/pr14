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
    public class RoomClassesController : ControllerBase
    {
        private readonly HotelsContext _context;

        public RoomClassesController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/RoomClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomClass>>> GetRoomClasses()
        {
          if (_context.RoomClasses == null)
          {
              return NotFound();
          }
            return await _context.RoomClasses.ToListAsync();
        }

        // GET: api/RoomClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomClass>> GetRoomClass(int id)
        {
          if (_context.RoomClasses == null)
          {
              return NotFound();
          }
            var roomClass = await _context.RoomClasses.FindAsync(id);

            if (roomClass == null)
            {
                return NotFound();
            }

            return roomClass;
        }

        // PUT: api/RoomClasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomClass(int id, RoomClass roomClass)
        {
            if (id != roomClass.IdRoomClass)
            {
                return BadRequest();
            }

            _context.Entry(roomClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomClassExists(id))
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

        // POST: api/RoomClasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomClass>> PostRoomClass(RoomClass roomClass)
        {
          if (_context.RoomClasses == null)
          {
              return Problem("Entity set 'HotelsContext.RoomClasses'  is null.");
          }
            _context.RoomClasses.Add(roomClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomClass", new { id = roomClass.IdRoomClass }, roomClass);
        }

        // DELETE: api/RoomClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomClass(int id)
        {
            if (_context.RoomClasses == null)
            {
                return NotFound();
            }
            var roomClass = await _context.RoomClasses.FindAsync(id);
            if (roomClass == null)
            {
                return NotFound();
            }

            _context.RoomClasses.Remove(roomClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("LogicDel")]
        public IActionResult LogicDeleteAnimal(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.RoomClasses.FirstOrDefault(t => t.IdRoomClass == id);
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
                var user = _context.RoomClasses.FirstOrDefault(t => t.IdRoomClass == id);
                if (user != null)
                {
                    user.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("paged-logs")]
        public async Task<ActionResult<List<List<RoomClass>>>> GetPagedAnimalLogs(int Pages, int Entities)
        {
            var totalLogsCount = await _context.RoomClasses.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalLogsCount / Entities);

            var answer = new List<List<RoomClass>>();

            var currentId = 1;
            while (true)
            {
                var logs = await _context.RoomClasses
                    .Where(log => log.IdRoomClass >= currentId)
                    .OrderBy(log => log.IdRoomClass)
                    .Take(Entities)
                    .ToListAsync();

                if (!logs.Any())
                {
                    break;
                }

                var pageLogs = new List<RoomClass>(logs);
                answer.Add(pageLogs);

                if (answer.Count == Pages || logs.Count < Entities)
                {
                    break;
                }

                currentId = logs.Last().IdRoomClass + 1;
            }

            return answer;
        }

        private bool RoomClassExists(int id)
        {
            return (_context.RoomClasses?.Any(e => e.IdRoomClass == id)).GetValueOrDefault();
        }
    }
}
