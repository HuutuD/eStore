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
    public class OrderRepository : IOrderRepository
    {
      
        public async Task DeleteOrderAsync(Order p) => await OrderDAO.DeleteOrderAsync(p);
        public async Task<List<Category>> GetCategoriesAsync() => await CategoryDAO.GetCategoriesAsync();
        public async Task<Order> GetOrderByIdAsync(int id) => await OrderDAO.FindOrderByIdAsync(id);
        public async Task<List<OrderDto>> GetOrdersAsync() => await OrderDAO.GetOrdersAsync();
        public async Task SaveOrderAsync(Order p) => await OrderDAO.SaveOrderAsync(p);
        public async Task UpdateOrderAsync(Order p) => await OrderDAO.UpdateOrderAsync(p);
    }
}
