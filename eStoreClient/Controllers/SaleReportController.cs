using eStoreClient.Models;
using eStoreClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace eStoreClient.Controllers
{
    public class SaleReportController : Controller
    {
        private readonly SalesReportService _salesReportService;

        public SaleReportController(SalesReportService salesReportService)
        {
            _salesReportService = salesReportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var model = new SaleReportViewModel
            {
                StartDate = startDate ?? DateTime.Now.AddMonths(-1), // Default to last month if no date provided
                EndDate = endDate ?? DateTime.Now
            };

            if (model.StartDate <= model.EndDate)
            {
                model.SalesReport = await _salesReportService.GetSalesReportAsync(model.StartDate.Value, model.EndDate.Value);
            }

            return View(model);
        }
    }
}



    
