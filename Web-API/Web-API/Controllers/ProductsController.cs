using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Web_API.Dtos;
using Web_API.Models;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly WebApidbContext _context;

        public ProductsController(WebApidbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<ProductDto> GetProducts(int pageNumber = 0, int pageSize = 10, int? categoryId = null)
        {
            var productList = new List<ProductDto>();

            var products = _context.Products.AsQueryable();
            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            products = products.Skip(pageNumber * pageSize).Take(pageSize);

            foreach (var product in products.ToList())
            {
                productList.Add(new ProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    SupplierId = product.SupplierId,
                    CategoryId = product.CategoryId,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                });
            }
            return productList;
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var result = new ProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    SupplierId = product.SupplierId,
                    CategoryId = product.CategoryId,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                };
                return result;
            }
        }

        // PUT: api/Products/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto productdto)
        {
            if (id != productdto.ProductId)
            {
                return BadRequest();
            }

            Product product = new Product
            {
                ProductId = productdto.ProductId,
                ProductName = productdto.ProductName,
                SupplierId = productdto.SupplierId,
                CategoryId = productdto.CategoryId,
                QuantityPerUnit = productdto.QuantityPerUnit,
                UnitPrice = productdto.UnitPrice,
                UnitsInStock = productdto.UnitsInStock,
                UnitsOnOrder = productdto.UnitsOnOrder,
                ReorderLevel = productdto.ReorderLevel,
                Discontinued = productdto.Discontinued,
            };

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto productdto)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'WebApidbContext.Products'  is null.");
            }
            Product product = new Product
            {
                ProductName = productdto.ProductName,
                SupplierId = productdto.SupplierId,
                CategoryId = productdto.CategoryId,
                QuantityPerUnit = productdto.QuantityPerUnit,
                UnitPrice = productdto.UnitPrice,
                UnitsInStock = productdto.UnitsInStock,
                UnitsOnOrder = productdto.UnitsOnOrder,
                ReorderLevel = productdto.ReorderLevel,
                Discontinued = productdto.Discontinued,
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
