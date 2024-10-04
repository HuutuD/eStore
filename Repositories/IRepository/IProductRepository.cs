using BussinessObject;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IProductRepository
    {
        Task SaveProductAsync(Product p);
        Task<Product> GetProductByIdAsync(int id);
        Task DeleteProductAsync(Product p);
        Task UpdateProductAsync(Product p);
        Task<List<ProductDto>> GetProductsAsync();
        Task<List<ProductDto>> SearchProductsAsync(string searchTerm);
    }
}
