using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailsDAO
    {
        public static async Task<List<OrderDetail>> GetOrderDetailsAsync()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                using (var context = new MyDbContext())
                {
                    orderDetails = await context.OrderDetails.AsNoTracking().ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }

        public static async Task<OrderDetail> FindOrderDetailByIdAsync(int orderDetailId)
        {
            OrderDetail orderDetail = null;
            try
            {
                using (var context = new MyDbContext())
                {
                    orderDetail = await context.OrderDetails.SingleOrDefaultAsync(x => x.Id == orderDetailId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public static async Task SaveOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    await context.OrderDetails.AddAsync(orderDetail);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry(orderDetail).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var existingOrderDetail = await context.OrderDetails.SingleOrDefaultAsync(c => c.Id == orderDetail.Id);
                    if (existingOrderDetail != null)
                    {
                        context.OrderDetails.Remove(existingOrderDetail);
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
