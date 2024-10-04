using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _context;

        public OrderDetailController(IOrderDetailRepository context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            var OrderDetails = await _context.GetOrderDetailsAsync();
            if (OrderDetails == null || !OrderDetails.Any())
            {
                return BadRequest();
            }
            return Ok(OrderDetails);
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {

            var OrderDetail = await _context.GetOrderDetailByIdAsync(id);
            if (OrderDetail == null)
            {
                return BadRequest();
            }
            return Ok(OrderDetail);
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail OrderDetail)
        {
            if (id != OrderDetail.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.UpdateOrderDetailAsync(OrderDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderDetailExists(id))
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

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail OrderDetail)
        {

            await _context.SaveOrderDetailAsync(OrderDetail);
            return CreatedAtAction(nameof(GetOrderDetail), new { id = OrderDetail.Id }, OrderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var OrderDetail = await _context.GetOrderDetailByIdAsync(id);
            if (OrderDetail == null)
            {
                return BadRequest();
            }

            await _context.DeleteOrderDetailAsync(OrderDetail);
            return NoContent();
        }

        private async Task<bool> OrderDetailExists(int id)
        {
            var OrderDetail = await _context.GetOrderDetailByIdAsync(id);
            return OrderDetail != null;
        }
    }
}
