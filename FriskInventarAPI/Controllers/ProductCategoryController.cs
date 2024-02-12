using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FriskInventarAPI;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FriskInventarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly FriskInventarContext _context;

        public ProductCategoryController(FriskInventarContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        // GET: api/ProductCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return productCategory;
        }

        // GET: api/ProductCategory/
        [HttpGet("FridgeContent/{fridgeId}")]
        public async Task<ActionResult<Dictionary<string, List<Product>>>> getProductCategory(int fridgeId) {
            /*var productCategories = await _context.ProductCategories.Where(productCategory => productCategory.Product.FridgeId == id)
            .Include(productCategory => productCategory.Product)
            
            .Include(productCategory => productCategory.Category).ToListAsync();*/
            
            var result = await _context.ProductCategories
                .Include(pc => pc.Category.ProductCategories)
                .Include(pc2 => pc2.Product)
                .Where(pc => pc.Category.FridgeId == fridgeId)
                .ToListAsync();
                //.Include<ProductCategory>(pc => pc.Product)
            Dictionary<string, List<Product>> records = new();
            foreach (var category in result) {
                if (!records.ContainsKey(category.Category.CategoryName))
                {
                    records.Add(category.Category.CategoryName, new());
                }
                foreach (var pc in category.Category.ProductCategories) {
                    var product = pc.Product;
                    if (!records[category.Category.CategoryName].Contains(product))
                    {
                        records[category.Category.CategoryName].Add(product);
                    }
                }
            }

            return records;
        }

        // PUT: api/ProductCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.ProductCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST: api/ProductCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductCategory", new { id = productCategory.ProductCategoryId }, productCategory);
        }

        // DELETE: api/ProductCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCategoryExists(int id)
        {
            return _context.ProductCategories.Any(e => e.ProductCategoryId == id);
        }
    }
}
