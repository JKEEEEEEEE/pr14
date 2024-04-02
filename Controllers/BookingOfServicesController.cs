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
    public class BookingOfServicesController : ControllerBase
    {
        private readonly HotelsContext _context;

        public BookingOfServicesController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/BookingOfServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingOfService>>> GetBookingOfServices()
        {
          if (_context.BookingOfServices == null)
          {
              return NotFound();
          }
            return await _context.BookingOfServices.ToListAsync();
        }

        // GET: api/BookingOfServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingOfService>> GetBookingOfService(int id)
        {
          if (_context.BookingOfServices == null)
          {
              return NotFound();
          }
            var bookingOfService = await _context.BookingOfServices.FindAsync(id);

            if (bookingOfService == null)
            {
                return NotFound();
            }

            return bookingOfService;
        }

        // PUT: api/BookingOfServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingOfService(int id, BookingOfService bookingOfService)
        {
            if (id != bookingOfService.IdBookingOfServices)
            {
                return BadRequest();
            }

            _context.Entry(bookingOfService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingOfServiceExists(id))
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

        // POST: api/BookingOfServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingOfService>> PostBookingOfService(BookingOfService bookingOfService)
        {
          if (_context.BookingOfServices == null)
          {
              return Problem("Entity set 'HotelsContext.BookingOfServices'  is null.");
          }
            _context.BookingOfServices.Add(bookingOfService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookingOfService", new { id = bookingOfService.IdBookingOfServices }, bookingOfService);
        }

        // DELETE: api/BookingOfServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingOfService(int id)
        {
            if (_context.BookingOfServices == null)
            {
                return NotFound();
            }
            var bookingOfService = await _context.BookingOfServices.FindAsync(id);
            if (bookingOfService == null)
            {
                return NotFound();
            }

            _context.BookingOfServices.Remove(bookingOfService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("LogicDel")]
        public IActionResult LogicDeleteAnimal(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.BookingOfServices.FirstOrDefault(t => t.IdBookingOfServices == id);
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
                var user = _context.BookingOfServices.FirstOrDefault(t => t.IdBookingOfServices == id);
                if (user != null)
                {
                    user.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("paged-logs")]
        public async Task<ActionResult<List<List<BookingOfService>>>> GetPagedAnimalLogs(int Pages, int Entities)
        {
            var totalLogsCount = await _context.BookingOfServices.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalLogsCount / Entities);

            var answer = new List<List<BookingOfService>>();

            var currentId = 1;
            while (true)
            {
                var logs = await _context.BookingOfServices
                    .Where(log => log.IdBookingOfServices >= currentId)
                    .OrderBy(log => log.IdBookingOfServices)
                    .Take(Entities)
                    .ToListAsync();

                if (!logs.Any())
                {
                    break;
                }

                var pageLogs = new List<BookingOfService>(logs);
                answer.Add(pageLogs);

                if (answer.Count == Pages || logs.Count < Entities)
                {
                    break;
                }

                currentId = logs.Last().IdBookingOfServices + 1;
            }

            return answer;
        }



        private bool BookingOfServiceExists(int id)
        {
            return (_context.BookingOfServices?.Any(e => e.IdBookingOfServices == id)).GetValueOrDefault();
        }
    }
}
