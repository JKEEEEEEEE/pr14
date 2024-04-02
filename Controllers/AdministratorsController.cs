using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotels.Models;
using System.Text;
using System.Security.Cryptography;

namespace Hotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public AdministratorsController(HotelsContext context)
        {
            _context = context;
        }

        public static byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // GET: api/Administrators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrator>>> GetAdministrators()
        {
            if (_context.Administrators == null)
            {
                return NotFound();
            }
            return await _context.Administrators.ToListAsync();
        }

        // GET: api/Administrators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrator>> GetAdministrator(int id)
        {
            if (_context.Administrators == null)
            {
                return NotFound();
            }
            var administrator = await _context.Administrators.FindAsync(id);

            if (administrator == null)
            {
                return NotFound();
            }

            return administrator;
        }

        // PUT: api/Administrators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrator(int id, Administrator administrator)
        {
            if (id != administrator.IdAdministrator)
            {
                return BadRequest();
            }

            _context.Entry(administrator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
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

        // POST: api/Administrators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Administrator>> PostAdministrator(Administrator administrator)
        {
            byte[] Salt = GenerateSalt(20);
            administrator.Salt = Convert.ToBase64String(Salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(administrator.PasswordAdministrator);
            byte[] hashedBytes = new Rfc2898DeriveBytes(passwordBytes, Salt, 10000).GetBytes(32);
            administrator.PasswordAdministrator = Convert.ToBase64String(hashedBytes);

            if (_context.Administrators == null)
            {
                return Problem("Entity set 'HotelsContext.Administrators'  is null.");
            }
            _context.Administrators.Add(administrator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministrator", new { id = administrator.IdAdministrator }, administrator);
        }

        // DELETE: api/Administrators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrator(int id)
        {
            if (_context.Administrators == null)
            {
                return NotFound();
            }
            var administrator = await _context.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }

            _context.Administrators.Remove(administrator);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("LogicDel")]
        public IActionResult LogicDeleteUser(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.Administrators.FirstOrDefault(t => t.IdAdministrator == id);
                if (user != null)
                {
                    user.IsDeleted = true;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("LogicRest")]
        public IActionResult LogicRestoreUser(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.Administrators.FirstOrDefault(t => t.IdAdministrator == id);
                if (user != null)
                {
                    user.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("paged-logs")]
        public async Task<ActionResult<List<List<Administrator>>>> GetPagedUserLogs(int Pages, int Entities)
        {
            var totalLogsCount = await _context.Administrators.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalLogsCount / Entities);

            var answer = new List<List<Administrator>>();

            var currentId = 1;
            while (true)
            {
                var logs = await _context.Administrators
                    .Where(log => log.IdAdministrator >= currentId)
                    .OrderBy(log => log.IdAdministrator)
                    .Take(Entities)
                    .ToListAsync();

                if (!logs.Any())
                {
                    break;
                }

                var pageLogs = new List<Administrator>(logs);
                answer.Add(pageLogs);

                if (answer.Count == Pages || logs.Count < Entities)
                {
                    break;
                }

                currentId = logs.Last().IdAdministrator + 1;
            }

            return answer;
        }

        // AUTH: api/Users/5
        [HttpGet("{EmailUser}/{Password}")]
        public async Task<ActionResult<string>> Authorization(string EmailUser, string Password)
        {
            var users = await _context.Administrators.Where(u => u.MailAdministrator == EmailUser).ToListAsync();

            if (users.Count == 0)
            {
                // пользователь не найден
                return NotFound();
            }
            else if (users.Count > 1)
            {
                // обнаружено несколько пользователей с таким именем
                return BadRequest("Multiple usernames detected");
            }

            var user = users[0];
            // преобразовываем строку Salt в массив байтов
            byte[] saltBytes = Convert.FromBase64String(user.Salt);

            // преобразовываем строку Password в массив байтов
            byte[] passwordBytes = Encoding.UTF8.GetBytes(Password);

            // вычисляем хеш пароля с помощью соли и 10000 итераций
            byte[] hashBytes = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000).GetBytes(32);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            if (hashedPassword == user.PasswordAdministrator)
            {
                // пароль совпадает, генерируем случайный токен и добавляем его в базу данных
                string token;
                Token existingToken;

                do
                {
                    token = Guid.NewGuid().ToString();
                    existingToken = await _context.Tokens.FirstOrDefaultAsync(t => t.Token1 == token);
                }
                while (existingToken != null);

                // создаем новую запись Token и сохраняем ее в базу данных
                // создаем новую запись Token и сохраняем ее в базу данных
                Token tok = new Token();
                tok.Token1 = token;
                tok.TokenDatetime = DateTime.Now;
                _context.Tokens.Add(tok);
                await _context.SaveChangesAsync();

                return token;
            }
            else
            {
                // пароль не совпадает
                return BadRequest("Неправильный пароль");
            }
        }

        [HttpPut("Password_Change")]
        public async Task<IActionResult> PutUser(int id, string New_password)
        {
            var user = await _context.Administrators.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            // хешируем новый пароль
            byte[] Salt = GenerateSalt(20);
            user.Salt = Convert.ToBase64String(Salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(New_password);
            byte[] hashBytes = new Rfc2898DeriveBytes(passwordBytes, Salt, 10000).GetBytes(32);
            user.PasswordAdministrator = Convert.ToBase64String(hashBytes);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
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

        // GET: api/Users
        [HttpGet("auth_key")]
        public async Task<ActionResult<string>> GetAuthKey(string LoginUser)
        {
            var user = await _context.Administrators.SingleOrDefaultAsync(u => u.MailAdministrator == LoginUser);
            if (user == null)
            {
                return NotFound($"Admin with login '{LoginUser}' was not found.");
            }
            else if (_context.Administrators.Count(u => u.MailAdministrator == LoginUser) > 1)
            {
                return BadRequest($"Multiple admins with login '{LoginUser}' were found.");
            }

            string salt = user.Salt;
            if (string.IsNullOrEmpty(salt))
            {
                return BadRequest($"Salt for user with login '{LoginUser}' is missing or empty.");
            }

            byte[] saltBytes = Encoding.UTF8.GetBytes(salt.Substring(0, Math.Min(salt.Length, 5)));
            byte[] reverseSalt = saltBytes.Reverse().ToArray();
            string hashedReverse = Convert.ToBase64String(reverseSalt);

            return hashedReverse;
        }


        // GET: api/Users
        [HttpGet("authentication")]
        public async Task<ActionResult<string>> GetAuthentication(string LoginUser, string AuthKey)
        {
            // Retrieve the user's password salt from the database
            var user = await _context.Administrators.FirstOrDefaultAsync(u => u.MailAdministrator == LoginUser);
            if (user == null)
            {
                return BadRequest("Invalid LoginUser");
            }
            var salt = user.Salt;

            // Compute the AuthKey from the password salt
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt.Substring(0, Math.Min(salt.Length, 5)));
            byte[] reverseSalt = saltBytes.Reverse().ToArray();
            string hashedReverse = Convert.ToBase64String(reverseSalt);

            // Check if the computed AuthKey matches the provided AuthKey
            if (hashedReverse != AuthKey)
            {
                return BadRequest("Invalid AuthKey");
            }

            // Generate a random token and add it to the database
            string token;
            Token existingToken;

            do
            {
                token = Guid.NewGuid().ToString();
                existingToken = await _context.Tokens.FirstOrDefaultAsync(t => t.Token1 == token);
            }
            while (existingToken != null);

            Token tok = new Token();
            tok.Token1 = token;
            tok.TokenDatetime = DateTime.Now;
            _context.Tokens.Add(tok);
            await _context.SaveChangesAsync();

            return token;


        }

        private bool AdministratorExists(int id)
        {
            return (_context.Administrators?.Any(e => e.IdAdministrator == id)).GetValueOrDefault();
        }
    }
}
