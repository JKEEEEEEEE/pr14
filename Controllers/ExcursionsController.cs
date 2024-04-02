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
    public class ExcursionsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public ExcursionsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/Excursions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Excursion>>> GetExcursions()
        {
          if (_context.Excursions == null)
          {
              return NotFound();
          }
            return await _context.Excursions.ToListAsync();
        }

        // GET: api/Excursions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Excursion>> GetExcursion(int id)
        {
          if (_context.Excursions == null)
          {
              return NotFound();
          }
            var excursion = await _context.Excursions.FindAsync(id);

            if (excursion == null)
            {
                return NotFound();
            }

            return excursion;
        }

        // PUT: api/Excursions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExcursion(int id, Excursion excursion)
        {
            if (id != excursion.IdExcursion)
            {
                return BadRequest();
            }

            _context.Entry(excursion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExcursionExists(id))
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

        // POST: api/Excursions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Excursion>> PostExcursion(Excursion excursion)
        {
          if (_context.Excursions == null)
          {
              return Problem("Entity set 'HotelsContext.Excursions'  is null.");
          }
            _context.Excursions.Add(excursion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExcursion", new { id = excursion.IdExcursion }, excursion);
        }

        // DELETE: api/Excursions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExcursion(int id)
        {
            if (_context.Excursions == null)
            {
                return NotFound();
            }
            var excursion = await _context.Excursions.FindAsync(id);
            if (excursion == null)
            {
                return NotFound();
            }

            _context.Excursions.Remove(excursion);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("LogicDel")]
        public IActionResult LogicDeleteAnimal(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.Excursions.FirstOrDefault(t => t.IdExcursion == id);
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
                var user = _context.Excursions.FirstOrDefault(t => t.IdExcursion == id);
                if (user != null)
                {
                    user.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("paged-logs")]
        public async Task<ActionResult<List<List<Excursion>>>> GetPagedAnimalLogs(int Pages, int Entities)
        {
            var totalLogsCount = await _context.Excursions.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalLogsCount / Entities);

            var answer = new List<List<Excursion>>();

            var currentId = 1;
            while (true)
            {
                var logs = await _context.Excursions
                    .Where(log => log.IdExcursion >= currentId)
                    .OrderBy(log => log.IdExcursion)
                    .Take(Entities)
                    .ToListAsync();

                if (!logs.Any())
                {
                    break;
                }

                var pageLogs = new List<Excursion>(logs);
                answer.Add(pageLogs);

                if (answer.Count == Pages || logs.Count < Entities)
                {
                    break;
                }

                currentId = logs.Last().IdExcursion + 1;
            }

            return answer;
        }

        [HttpGet("Filtr")]
        public async Task<ActionResult<IEnumerable<Excursion>>> FiltrationPlane(
         int Search)
        {


            var excursions = _context.Excursions.Where(x => x.IsDeleted == false && x.HotelId == Search).ToList();
            foreach (var excursion in excursions)
            {
                var ph = await _context.Hotels.FindAsync(excursion.HotelId);

                excursion.Hotel = ph;

            }
            return excursions;

        }

        [HttpGet("Sort")]
        public async Task<ActionResult<IEnumerable<Excursion>>> Sort(int pageNumber = 1, int pageSize = 20)
        {

            var excursions = _context.Excursions.Where(x => x.IsDeleted == false).OrderBy(y => y.RouteExcursion).Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            foreach (var excursion in excursions)
            {
                var mod = await _context.Hotels.FindAsync(excursion.HotelId);
                excursion.Hotel = mod;

            }

            return Ok(excursions);


        }

        [HttpGet("Poisk")]
        public async Task<ActionResult<IEnumerable<Excursion>>> SearchPlane(string Search, int pageNumber = 1, int pageSize = 20)
        {


            var excursions = _context.Excursions.Where(x => x.IsDeleted == false && x.SecondNameGuideExcursion.StartsWith(Search)).Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            foreach (var excursion in excursions)
            {
                var mod = await _context.Hotels.FindAsync(excursion.HotelId);
                excursion.Hotel = mod;
            }
            return Ok(excursions);
        }

        private bool ExcursionExists(int id)
        {
            return (_context.Excursions?.Any(e => e.IdExcursion == id)).GetValueOrDefault();
        }
    }
}
