using BussinessObject;
using DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        public static async Task<List<ProductDto>> GetProductsAsync()
        {

            try
            {
                using (var context = new MyDbContext())
                {
                    var products = await context.Products
                        .AsNoTracking()
                        .Select(p => new ProductDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            CategoryName = p.Categories.Name,
                            CategoryId = p.CategoryId,
                            Weight = p.Weight,
                            UnitPrice = p.UnitPrice,
                            UnitInstock = p.UnitInstock

                        })
                        .ToListAsync();

                    return products;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static async Task<Product> FindProductByIdAsync(int prodId)
        {
            Product product = null;
            try
            {
                using (var context = new MyDbContext())
                {
                    product = await context.Products.SingleOrDefaultAsync(x => x.Id == prodId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public static async Task SaveProductAsync(Product product)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    await context.Products.AddAsync(product);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateProductAsync(Product product)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry(product).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteProductAsync(Product product)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var existingProduct = await context.Products.SingleOrDefaultAsync(c => c.Id == product.Id);
                    if (existingProduct != null)
                    {
                        context.Products.Remove(existingProduct);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetProductsAsync();
            }

            try
            {
                using (var context = new MyDbContext())
                {

                    bool isNumeric = int.TryParse(searchTerm, out int numericValue);

                    var products = await context.Products
                        .AsNoTracking()
                        .Where(p => p.Name.ToLower().Contains(searchTerm.ToLower().Trim()) || (isNumeric && p.UnitPrice > numericValue))
                        .Select(p => new ProductDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            CategoryName = p.Categories.Name,
                            CategoryId = p.CategoryId,
                            Weight = p.Weight,
                            UnitPrice = p.UnitPrice,
                            UnitInstock = p.UnitInstock
                        })
                        .ToListAsync();

                    return products;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
