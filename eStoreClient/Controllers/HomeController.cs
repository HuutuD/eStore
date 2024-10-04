using DataAccess.DTO;
using eStoreClient.Models;
using eStoreClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService<ProductDto> _productDtoService;
        private readonly string ProductAPIUrl;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger,
            ApiService<ProductDto> productDtoService,
            IOptions<ApiUrls> apiUrls)
        {
            _logger = logger;
            _productDtoService = productDtoService;
            ProductAPIUrl = apiUrls.Value.ProductAPIUrl;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> products = await _productDtoService.GetAllAsync(ProductAPIUrl);
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
