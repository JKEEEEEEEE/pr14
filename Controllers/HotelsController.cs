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
    public class HotelsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public HotelsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
          if (_context.Hotels == null)
          {
              return NotFound();
          }
            return await _context.Hotels.ToListAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
          if (_context.Hotels == null)
          {
              return NotFound();
          }
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.IdHotel)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
          if (_context.Hotels == null)
          {
              return Problem("Entity set 'HotelsContext.Hotels'  is null.");
          }
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.IdHotel }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context.Hotels == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpPut("LogicDel")]
        public IActionResult LogicDeleteAnimal(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.Hotels.FirstOrDefault(t => t.IdHotel == id);
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
                var user = _context.Hotels.FirstOrDefault(t => t.IdHotel == id);
                if (user != null)
                {
                    user.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("paged-logs")]
        public async Task<ActionResult<List<List<Hotel>>>> GetPagedAnimalLogs(int Pages, int Entities)
        {
            var totalLogsCount = await _context.Hotels.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalLogsCount / Entities);

            var answer = new List<List<Hotel>>();

            var currentId = 1;
            while (true)
            {
                var logs = await _context.Hotels
                    .Where(log => log.IdHotel >= currentId)
                    .OrderBy(log => log.IdHotel)
                    .Take(Entities)
                    .ToListAsync();

                if (!logs.Any())
                {
                    break;
                }

                var pageLogs = new List<Hotel>(logs);
                answer.Add(pageLogs);

                if (answer.Count == Pages || logs.Count < Entities)
                {
                    break;
                }

                currentId = logs.Last().IdHotel + 1;
            }

            return answer;
        }
        private bool HotelExists(int id)
        {
            return (_context.Hotels?.Any(e => e.IdHotel == id)).GetValueOrDefault();
        }
    }
}
