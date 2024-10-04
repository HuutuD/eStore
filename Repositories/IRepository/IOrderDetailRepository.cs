using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IOrderDetailRepository
    {
        Task SaveOrderDetailAsync(OrderDetail p);
        Task<OrderDetail> GetOrderDetailByIdAsync(int id);
        Task DeleteOrderDetailAsync(OrderDetail p);
        Task UpdateOrderDetailAsync(OrderDetail p);
        Task<List<Category>> GetCategoriesAsync();
        Task<List<OrderDetail>> GetOrderDetailsAsync();
    }
}
