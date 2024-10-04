using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BussinessObject;
using Microsoft.AspNetCore.Authorization;
using eStoreClient.Models;
using Microsoft.Extensions.Options;
using DataAccess.DTO;
using eStoreClient.Services;

namespace eStoreClient.Controllers
{
    [AdminOnly]
    public class CategoriesController : Controller
    {
        private readonly ApiService<Category> _CategoryService;
        private readonly ApiService<CategoryDto> _CategoryDtoService;
        private readonly string CategoriesAPIUrl;

        public CategoriesController(ApiService<Category> CategoryService,
                                    IOptions<ApiUrls> apiUrls,
                                    ApiService<CategoryDto> categoryDtoService)
        {
            _CategoryService = CategoryService;
            CategoriesAPIUrl = apiUrls.Value.CategoriesAPIUrl;
            _CategoryDtoService = categoryDtoService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            List<CategoryDto> categories = await _CategoryDtoService.GetAllAsync(CategoriesAPIUrl);
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            bool isCreated = await _CategoryService.CreateAsync(CategoriesAPIUrl, category);
            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating Category. Please try again.");
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _CategoryService.GetByIdAsync(CategoriesAPIUrl, id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            bool isUpdated = await _CategoryService.UpdateAsync(CategoriesAPIUrl, category, category.Id);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error updating Category. Please try again.");
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _CategoryService.DeleteAsync(CategoriesAPIUrl, id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {
            Category category = await _CategoryService.GetByIdAsync(CategoriesAPIUrl, id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    }
}
