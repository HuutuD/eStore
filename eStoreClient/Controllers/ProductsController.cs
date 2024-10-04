using BussinessObject;
using DataAccess.DTO;
using eStoreClient.Models;
using eStoreClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    [AdminOnly]
    public class ProductsController : Controller
    {
        private readonly ApiService<ProductDto> _productDtoService;
        private readonly ApiService<Product> _productService;
        private readonly ApiService<Category> _categoryService;
        private readonly string ProductAPIUrl;
        private readonly string CategoriesAPIUrl;

        public ProductsController(
            ApiService<ProductDto> productDtoService,
            ApiService<Product> productService,
            ApiService<Category> categoryService,
            IOptions<ApiUrls> apiUrls)
        {
            _productDtoService = productDtoService;
            _productService = productService;
            _categoryService = categoryService;


            ProductAPIUrl = apiUrls.Value.ProductAPIUrl;
            CategoriesAPIUrl = apiUrls.Value.CategoriesAPIUrl;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProductDto> products = new List<ProductDto>();



            if (TempData["SearchResults"] != null)
            {
                var searchResultsJson = TempData["SearchResults"].ToString();
                products = JsonSerializer.Deserialize<List<ProductDto>>(searchResultsJson);
            }
            else
            {
                products = await _productDtoService.GetAllAsync(ProductAPIUrl);
            }
            return View(products);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Category> categories = await _categoryService.GetAllAsync(CategoriesAPIUrl);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = await _categoryService.GetAllAsync(CategoriesAPIUrl);
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }

            bool isCreated = await _productService.CreateAsync(ProductAPIUrl, product);
            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating product. Please try again.");
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _productService.GetByIdAsync(ProductAPIUrl, id);
            if (product == null)
            {
                return NotFound();
            }

            List<Category> categories = await _categoryService.GetAllAsync(CategoriesAPIUrl);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = await _categoryService.GetAllAsync(CategoriesAPIUrl);
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }

            bool isUpdated = await _productService.UpdateAsync(ProductAPIUrl, product, product.Id);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error updating product. Please try again.");
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _productService.DeleteAsync(ProductAPIUrl, id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {

            Product product = await _productService.GetByIdAsync(ProductAPIUrl, id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpGet("search")]
        public async Task<ActionResult<List<ProductDto>>> Search(string searchTerm)
        {

            var products = await _productDtoService.SearchAsync(ProductAPIUrl, searchTerm);

            if (products == null || !products.Any())
            {
                return RedirectToAction("Index");
            }
            TempData["SearchResults"] = JsonSerializer.Serialize(products);

            return RedirectToAction("Index");
        }


    }
}
