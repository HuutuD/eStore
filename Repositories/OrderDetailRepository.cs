using BussinessObject;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public async Task DeleteOrderDetailAsync(OrderDetail p) => await OrderDetailsDAO.DeleteOrderDetailAsync(p);
        public async Task<List<Category>> GetCategoriesAsync() => await CategoryDAO.GetCategoriesAsync();
        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id) => await OrderDetailsDAO.FindOrderDetailByIdAsync(id);
        public async Task<List<OrderDetail>> GetOrderDetailsAsync() => await OrderDetailsDAO.GetOrderDetailsAsync();
        public async Task SaveOrderDetailAsync(OrderDetail p) => await OrderDetailsDAO.SaveOrderDetailAsync(p);
        public async Task UpdateOrderDetailAsync(OrderDetail p) => await OrderDetailsDAO.UpdateOrderDetailAsync(p);
    }
}
