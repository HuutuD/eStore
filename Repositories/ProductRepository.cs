using BussinessObject;
using DataAccess;
using DataAccess.DTO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        public async Task DeleteProductAsync(Product p) => await ProductDAO.DeleteProductAsync(p);
        public async Task<List<Category>> GetCategoriesAsync() => await CategoryDAO.GetCategoriesAsync();
        public async Task<Product> GetProductByIdAsync(int id) => await ProductDAO.FindProductByIdAsync(id);
        public async Task<List<ProductDto>> GetProductsAsync() => await ProductDAO.GetProductsAsync();
        public async Task SaveProductAsync(Product p) => await ProductDAO.SaveProductAsync(p);
        public async Task UpdateProductAsync(Product p) => await ProductDAO.UpdateProductAsync(p);
        public async Task<List<ProductDto>> SearchProductsAsync(string searchTerm) => await ProductDAO.SearchProductsAsync(searchTerm);
    }
}
