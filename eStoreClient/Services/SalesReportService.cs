using BussinessObject;
using Microsoft.EntityFrameworkCore;

namespace eStoreClient.Services
{
    public class SalesReportService
    {
        private readonly MyDbContext _context;

        public SalesReportService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalesReportDto>> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            var salesData = await _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .GroupBy(o => o.Id)
                .Select(g => new SalesReportDto
                {
                    ProductId = g.Key,
                    TotalSales = g.Sum(o => o.Freight)
                })
                .OrderByDescending(s => s.TotalSales)
                .ToListAsync();

            return salesData;
        }
    }

    public class SalesReportDto
    {
        public int ProductId { get; set; }
        public decimal TotalSales { get; set; }
 
    }

   


}
