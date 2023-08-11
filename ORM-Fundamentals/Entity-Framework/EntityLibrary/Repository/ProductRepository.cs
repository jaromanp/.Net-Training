using EntityLibrary.Interface;
using EntityLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EntityDatabaseContext _context;

        public ProductRepository(EntityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ArgumentException($"Product with Id {id} not found.");
            }

            return product;
        }

        public async Task UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var existingProduct = await _context.Products.FindAsync(product.Id);

            if (existingProduct == null)
            {
                throw new ArgumentException($"Product with Id {product.Id} not found.");
            }

            existingProduct.Description = product.Description;
            existingProduct.Weight = product.Weight;
            existingProduct.Height = product.Height;
            existingProduct.Width = product.Width;
            existingProduct.Length = product.Length;

            _context.Entry(existingProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ArgumentException($"Product with Id {id} not found.");
            }
            
            int orderCount = await _context.Orders.CountAsync(o => o.ProductId == id);

            if (orderCount > 0)
            {
                throw new InvalidOperationException($"Cannot delete Product with Id {id} as there are existing orders associated with it.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
