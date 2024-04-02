using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotels.Models;
using System.Security.Cryptography;
using System.Text;

namespace Hotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly HotelsContext _context;

        public ClientsController(HotelsContext context)
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

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.IdClient)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            byte[] Salt = GenerateSalt(20);
            client.Salt = Convert.ToBase64String(Salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(client.PasswordClient);
            byte[] hashedBytes = new Rfc2898DeriveBytes(passwordBytes, Salt, 10000).GetBytes(32);
            client.PasswordClient = Convert.ToBase64String(hashedBytes);

            if (_context.Clients == null)
            {
                return Problem("Entity set 'HotelsContext.Clients'  is null.");
            }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("LogicDel")]
        public IActionResult LogicDeleteUser(int[] ids)
        {
            foreach (int id in ids)
            {
                var user = _context.Clients.FirstOrDefault(t => t.IdClient == id);
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
                var user = _context.Clients.FirstOrDefault(t => t.IdClient == id);
                if (user != null)
                {
                    user.IsDeleted = false;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("paged-logs")]
        public async Task<ActionResult<List<List<Client>>>> GetPagedUserLogs(int Pages, int Entities)
        {
            var totalLogsCount = await _context.Clients.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalLogsCount / Entities);

            var answer = new List<List<Client>>();

            var currentId = 1;
            while (true)
            {
                var logs = await _context.Clients
                    .Where(log => log.IdClient >= currentId)
                    .OrderBy(log => log.IdClient)
                    .Take(Entities)
                    .ToListAsync();

                if (!logs.Any())
                {
                    break;
                }

                var pageLogs = new List<Client>(logs);
                answer.Add(pageLogs);

                if (answer.Count == Pages || logs.Count < Entities)
                {
                    break;
                }

                currentId = logs.Last().IdClient + 1;
            }

            return answer;
        }

        // AUTH: api/Users/5
        [HttpGet("{Mail}/{Pass}")]
        public async Task<ActionResult<string>> Authorization(string EmailUser, string Password)
        {
            var users = await _context.Clients.Where(u => u.MailClient == EmailUser).ToListAsync();

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

            if (hashedPassword == user.PasswordClient)
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
            var user = await _context.Clients.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            // хешируем новый пароль
            byte[] Salt = GenerateSalt(20);
            user.Salt = Convert.ToBase64String(Salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(New_password);
            byte[] hashBytes = new Rfc2898DeriveBytes(passwordBytes, Salt, 10000).GetBytes(32);
            user.PasswordClient = Convert.ToBase64String(hashBytes);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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
        [HttpGet("key")]
        public async Task<ActionResult<string>> GetAuthKey(string LoginUser)
        {
            var user = await _context.Clients.SingleOrDefaultAsync(u => u.MailClient == LoginUser);
            if (user == null)
            {
                return NotFound($"Admin with login '{LoginUser}' was not found.");
            }
            else if (_context.Clients.Count(u => u.MailClient == LoginUser) > 1)
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
        [HttpGet("authent")]
        public async Task<ActionResult<string>> GetAuthentication(string LoginUser, string AuthKey)
        {
            // Retrieve the user's password salt from the database
            var user = await _context.Clients.FirstOrDefaultAsync(u => u.MailClient == LoginUser);
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

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.IdClient == id)).GetValueOrDefault();
        }
    }
}
