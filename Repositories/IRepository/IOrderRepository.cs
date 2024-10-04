using BussinessObject;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IOrderRepository
    {
        Task SaveOrderAsync(Order p);
        Task<Order> GetOrderByIdAsync(int id);
        Task DeleteOrderAsync(Order p);
        Task UpdateOrderAsync(Order p);
        Task<List<OrderDto>> GetOrdersAsync();
    }
}
