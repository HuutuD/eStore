using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _context;


        public CategoriesController(ICategoryRepository context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var Categories = await _context.GetCategoriesAsync();
            if (Categories == null || !Categories.Any())
            {
                return BadRequest();
            }
            return Ok(Categories);
        }
        
    }
}
