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
    public class PostHistsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public PostHistsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/PostHists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostHist>>> GetPostHists()
        {
          if (_context.PostHists == null)
          {
              return NotFound();
          }
            return await _context.PostHists.ToListAsync();
        }

        // GET: api/PostHists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostHist>> GetPostHist(int id)
        {
          if (_context.PostHists == null)
          {
              return NotFound();
          }
            var postHist = await _context.PostHists.FindAsync(id);

            if (postHist == null)
            {
                return NotFound();
            }

            return postHist;
        }

        // PUT: api/PostHists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostHist(int id, PostHist postHist)
        {
            if (id != postHist.IdPostHist)
            {
                return BadRequest();
            }

            _context.Entry(postHist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostHistExists(id))
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

        // POST: api/PostHists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostHist>> PostPostHist(PostHist postHist)
        {
          if (_context.PostHists == null)
          {
              return Problem("Entity set 'HotelsContext.PostHists'  is null.");
          }
            _context.PostHists.Add(postHist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostHist", new { id = postHist.IdPostHist }, postHist);
        }

        // DELETE: api/PostHists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostHist(int id)
        {
            if (_context.PostHists == null)
            {
                return NotFound();
            }
            var postHist = await _context.PostHists.FindAsync(id);
            if (postHist == null)
            {
                return NotFound();
            }

            _context.PostHists.Remove(postHist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostHistExists(int id)
        {
            return (_context.PostHists?.Any(e => e.IdPostHist == id)).GetValueOrDefault();
        }
    }
}
