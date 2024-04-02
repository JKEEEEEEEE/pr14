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
    public class RoomCleaningHistsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public RoomCleaningHistsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/RoomCleaningHists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomCleaningHist>>> GetRoomCleaningHists()
        {
          if (_context.RoomCleaningHists == null)
          {
              return NotFound();
          }
            return await _context.RoomCleaningHists.ToListAsync();
        }

        // GET: api/RoomCleaningHists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomCleaningHist>> GetRoomCleaningHist(int id)
        {
          if (_context.RoomCleaningHists == null)
          {
              return NotFound();
          }
            var roomCleaningHist = await _context.RoomCleaningHists.FindAsync(id);

            if (roomCleaningHist == null)
            {
                return NotFound();
            }

            return roomCleaningHist;
        }

        // PUT: api/RoomCleaningHists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomCleaningHist(int id, RoomCleaningHist roomCleaningHist)
        {
            if (id != roomCleaningHist.IdRoomClassHist)
            {
                return BadRequest();
            }

            _context.Entry(roomCleaningHist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomCleaningHistExists(id))
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

        // POST: api/RoomCleaningHists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomCleaningHist>> PostRoomCleaningHist(RoomCleaningHist roomCleaningHist)
        {
          if (_context.RoomCleaningHists == null)
          {
              return Problem("Entity set 'HotelsContext.RoomCleaningHists'  is null.");
          }
            _context.RoomCleaningHists.Add(roomCleaningHist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomCleaningHist", new { id = roomCleaningHist.IdRoomClassHist }, roomCleaningHist);
        }

        // DELETE: api/RoomCleaningHists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomCleaningHist(int id)
        {
            if (_context.RoomCleaningHists == null)
            {
                return NotFound();
            }
            var roomCleaningHist = await _context.RoomCleaningHists.FindAsync(id);
            if (roomCleaningHist == null)
            {
                return NotFound();
            }

            _context.RoomCleaningHists.Remove(roomCleaningHist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomCleaningHistExists(int id)
        {
            return (_context.RoomCleaningHists?.Any(e => e.IdRoomClassHist == id)).GetValueOrDefault();
        }
    }
}
