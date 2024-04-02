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
    public class EmployeesController : ControllerBase
    {
        private readonly HotelsContext _context;

        public EmployeesController(HotelsContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.IdEmployee)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employees == null)
          {
              return Problem("Entity set 'HotelsContext.Employees'  is null.");
          }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.IdEmployee }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Filtr")]
        public async Task<ActionResult<IEnumerable<Employee>>> FiltrationPlane(
         int Search)
        {


            var employees = _context.Employees.Where(x => x.IsDeleted == false && x.PostId == Search).ToList();
            foreach (var employee in employees)
            {
                var pr = await _context.Posts.FindAsync(employee.PostId);

                employee.Post = pr;

            }
            return employees;

        }

        [HttpGet("Sort")]
        public async Task<ActionResult<IEnumerable<Employee>>> Sort(int pageNumber = 1, int pageSize = 20)
        {

            var employees = _context.Employees.Where(x => x.IsDeleted == false).OrderBy(y => y.FirstNameEmployee).Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            foreach (var employee in employees)
            {
                var mod = await _context.Posts.FindAsync(employee.PostId);
                employee.Post = mod;

            }

            return Ok(employees);


        }

        [HttpGet("Poisk")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchPlane(string Search, int pageNumber = 1, int pageSize = 20)
        {


            var employees = _context.Employees.Where(x => x.IsDeleted == false && x.SecondNameEmployee.StartsWith(Search)).Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            foreach (var employee in employees)
            {
                var mod = await _context.Posts.FindAsync(employee.PostId);
                employee.Post = mod;
            }
            return Ok(employees);
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.IdEmployee == id)).GetValueOrDefault();
        }
    }
}
