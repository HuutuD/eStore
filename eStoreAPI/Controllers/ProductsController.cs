using BussinessObject;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _context;


        public ProductsController(IProductRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(string searchTerm = null)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return Ok(await _context.GetProductsAsync());
            }

            var products = await _context.SearchProductsAsync(searchTerm);
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            var Product = await _context.GetProductByIdAsync(id);
            if (Product == null)
            {
                return BadRequest();
            }
            return Ok(Product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product Product)
        {
            if (id != Product.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.UpdateProductAsync(Product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product Product)
        {

            await _context.SaveProductAsync(Product);
            return CreatedAtAction(nameof(GetProduct), new { id = Product.Id }, Product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var Product = await _context.GetProductByIdAsync(id);
            if (Product == null)
            {
                return BadRequest();
            }

            await _context.DeleteProductAsync(Product);
            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            var Product = await _context.GetProductByIdAsync(id);
            return Product != null;
        }
    }
}
