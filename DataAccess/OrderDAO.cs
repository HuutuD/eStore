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
    public class OrderDAO
    {
        public static async Task<List<OrderDto>> GetOrdersAsync()
        {

            try
            {
                using (var context = new MyDbContext())
                {
                    var orders = await context.Orders.AsNoTracking()
                    .Select(p => new OrderDto
                    {
                        Id = p.Id,
                        MemberId = p.MemberId,
                        OrderDate = p.OrderDate,
                        RequiredDate = p.RequiredDate,
                        ShipDate = p.ShipDate,
                        Freight = p.Freight,
                        CompanyName = p.Members.CompanyName
                    }).ToListAsync();

                    return orders;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static async Task<Order> FindOrderByIdAsync(int orderId)
        {
            Order order = null;
            try
            {
                using (var context = new MyDbContext())
                {
                    order = await context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public static async Task SaveOrderAsync(Order order)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    await context.Orders.AddAsync(order);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateOrderAsync(Order order)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry(order).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteOrderAsync(Order order)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var existingOrder = await context.Orders.SingleOrDefaultAsync(c => c.Id == order.Id);
                    if (existingOrder != null)
                    {
                        context.Orders.Remove(existingOrder);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
