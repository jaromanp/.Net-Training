using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API.Dtos;
using Web_API.Models;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly WebApidbContext _context;

        public CategoriesController(WebApidbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<CategoryDto> GetCategories()
        {
            var categoriesList = new List<CategoryDto>();

            foreach (var category in _context.Categories.ToList())
            {
                categoriesList.Add(new CategoryDto
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Picture = Encoding.UTF8.GetString(category.Picture),
                });
            }
            return categoriesList;
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            } else
            {
                var result = new CategoryDto
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Picture = Encoding.UTF8.GetString(category.Picture),
                };
                return result;
            }
        }

        // PUT: api/Categories/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDto categorydto)
        {
            if (id != categorydto.CategoryId)
            {
                return BadRequest();
            }

            Category category = new Category
            {
                CategoryId = categorydto.CategoryId,
                CategoryName = categorydto.CategoryName,
                Description = categorydto.Description,
                Picture = Encoding.ASCII.GetBytes(categorydto.Picture),
            };

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDto categorydto)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'WebApidbContext.Categories'  is null.");
            }

            Category category = new Category
            {
                CategoryName = categorydto.CategoryName,
                Description = categorydto.Description,
                Picture = Encoding.ASCII.GetBytes(categorydto.Picture),
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
