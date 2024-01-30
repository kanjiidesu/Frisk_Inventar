using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FriskInventarAPI;

namespace FriskInventarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFridgeController : ControllerBase
    {
        private readonly FriskInventarContext _context;

        public UserFridgeController(FriskInventarContext context)
        {
            _context = context;
        }

        // GET: api/UserFridge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserFridge>>> GetUserFridges()
        {
            return await _context.UserFridges.ToListAsync();
        }

        // GET: api/UserFridge/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserFridge>> GetUserFridge(int id)
        {
            var userFridge = await _context.UserFridges.FindAsync(id);

            if (userFridge == null)
            {
                return NotFound();
            }

            return userFridge;
        }

        // GET: api/UserFridge/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<UserFridge>>> GetUserFridgeByUser(int userId)
        {
            // Include is a left join
            // We use left join to get more information,
            var userFridges = await _context.UserFridges.Where(userFridge => userFridge.UserId == userId)
            .Include(userFridge => userFridge.Fridge)
                .ThenInclude(fridge => fridge.Products)
            .Include(userFridge => userFridge.User).ToListAsync();

            if (userFridges == null)
            {
                return NotFound();
            }

            return userFridges;
        }

        // PUT: api/UserFridge/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserFridge(int id, UserFridge userFridge)
        {
            if (id != userFridge.UserFridgeId)
            {
                return BadRequest();
            }

            _context.Entry(userFridge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFridgeExists(id))
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

        // POST: api/UserFridge
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserFridge>> PostUserFridge(UserFridge userFridge)
        {
            _context.UserFridges.Add(userFridge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserFridge", new { id = userFridge.UserFridgeId }, userFridge);
        }

        // DELETE: api/UserFridge/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserFridge(int id)
        {
            var userFridge = await _context.UserFridges.FindAsync(id);
            if (userFridge == null)
            {
                return NotFound();
            }

            _context.UserFridges.Remove(userFridge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserFridgeExists(int id)
        {
            return _context.UserFridges.Any(e => e.UserFridgeId == id);
        }
    }
}
