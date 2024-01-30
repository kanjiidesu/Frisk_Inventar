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
    public class FridgeController : ControllerBase
    {
        private readonly FriskInventarContext _context;

        public FridgeController(FriskInventarContext context)
        {
            _context = context;
        }

        // GET: api/Fridge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fridge>>> GetFridges()
        {
            return await _context.Fridges.Include(fridge => fridge.Products).ToListAsync();
        }

        // GET: api/Fridge/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fridge>> GetFridge(int id)
        {
            // Include is a left join
            // We use left join to get more information, for example here we want a list of products and its info
            Fridge? fridge = await _context.Fridges.Include(fridge => fridge.Products).FirstOrDefaultAsync(fridge => fridge.FridgeId==id);

            if (fridge == null)
            {
                return NotFound();
            }

            return fridge;
        }

        // PUT: api/Fridge/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFridge(int id, Fridge fridge)
        {
            if (id != fridge.FridgeId)
            {
                return BadRequest();
            }

            _context.Entry(fridge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FridgeExists(id))
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

        // POST: api/Fridge
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fridge>> PostFridge(Fridge fridge)
        {
            _context.Fridges.Add(fridge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFridge", new { id = fridge.FridgeId }, fridge);
        }

        // DELETE: api/Fridge/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridge(int id)
        {
            var fridge = await _context.Fridges.FindAsync(id);
            if (fridge == null)
            {
                return NotFound();
            }

            _context.Fridges.Remove(fridge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FridgeExists(int id)
        {
            return _context.Fridges.Any(e => e.FridgeId == id);
        }
    }
}
