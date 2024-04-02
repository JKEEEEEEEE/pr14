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
    public class RoomClassHistsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public RoomClassHistsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/RoomClassHists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomClassHist>>> GetRoomClassHists()
        {
          if (_context.RoomClassHists == null)
          {
              return NotFound();
          }
            return await _context.RoomClassHists.ToListAsync();
        }

        // GET: api/RoomClassHists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomClassHist>> GetRoomClassHist(int id)
        {
          if (_context.RoomClassHists == null)
          {
              return NotFound();
          }
            var roomClassHist = await _context.RoomClassHists.FindAsync(id);

            if (roomClassHist == null)
            {
                return NotFound();
            }

            return roomClassHist;
        }

        // PUT: api/RoomClassHists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomClassHist(int id, RoomClassHist roomClassHist)
        {
            if (id != roomClassHist.IdRoomClassHist)
            {
                return BadRequest();
            }

            _context.Entry(roomClassHist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomClassHistExists(id))
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

        // POST: api/RoomClassHists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomClassHist>> PostRoomClassHist(RoomClassHist roomClassHist)
        {
          if (_context.RoomClassHists == null)
          {
              return Problem("Entity set 'HotelsContext.RoomClassHists'  is null.");
          }
            _context.RoomClassHists.Add(roomClassHist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomClassHist", new { id = roomClassHist.IdRoomClassHist }, roomClassHist);
        }

        // DELETE: api/RoomClassHists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomClassHist(int id)
        {
            if (_context.RoomClassHists == null)
            {
                return NotFound();
            }
            var roomClassHist = await _context.RoomClassHists.FindAsync(id);
            if (roomClassHist == null)
            {
                return NotFound();
            }

            _context.RoomClassHists.Remove(roomClassHist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomClassHistExists(int id)
        {
            return (_context.RoomClassHists?.Any(e => e.IdRoomClassHist == id)).GetValueOrDefault();
        }
    }
}
