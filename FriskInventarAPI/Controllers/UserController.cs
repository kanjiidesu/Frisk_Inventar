using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FriskInventarAPI;
using System.Net;
using FriskInventarAPI.SessionHandler;

namespace FriskInventarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FriskInventarContext _context;

        public UserController(FriskInventarContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id, string sessionId)
        {
            if (SessionManager.Check(sessionId) 
                && SessionManager.GetSession(sessionId).UserId == id)
            {
                var user = await _context.Users.FindAsync(id); 

                if (user == null)
                {
                    return NotFound();
                }

                return user;   
            }
            return null;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users.Any(e => e.UserName == user.UserName))
            {
                return BadRequest();
            }
            if (_context.Users.Any(e => e.Email == user.Email))
            {
                return BadRequest();
            }
            if (_context.Users.Any(e => e.Password == user.Password))
            {
                return BadRequest();
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // POST: api/Login
        [HttpPost("login")]
        public async Task<ActionResult<Session>> Login(UserLoginRequest loginRequest)
        {
            // Find the user based on the provided username
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginRequest.UserName);

            if (user == null)
            {
                // User not found
                return NotFound();
            }

            // Validate the password
            if (user.Password != loginRequest.Password)
            {
                // Incorrect password
                return Unauthorized();
            }
            Session session = new(user.UserId);
            SessionManager.Add(session);
            // Login successful
            return Ok(session);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
