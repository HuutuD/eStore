using BussinessObject;
using DataAccess.DTO;
using eStoreClient.Models;
using eStoreClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace eStoreClient.Controllers
{
    [AdminOnly]
    public class OrdersController : Controller
    {
        private readonly ApiService<Order> _OrderService;
        private readonly ApiService<OrderDto> _OrderDtoService;
        private readonly ApiService<Member> _MemberService;
        private readonly string OrdersAPIUrl;
        private readonly string MemberAPIUrl;

        public OrdersController(ApiService<Order> OrderService,
                        IOptions<ApiUrls> apiUrls,
                        ApiService<OrderDto> orderDtoService,
                         ApiService<Member> MemberService
                        )
        {
            _OrderService = OrderService;
            OrdersAPIUrl = apiUrls.Value.OrdersAPIUrl;
            _OrderDtoService = orderDtoService;
            _MemberService = MemberService;
            MemberAPIUrl = apiUrls.Value.MembersAPIUrl;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OrderDto> Orders = await _OrderDtoService.GetAllAsync(OrdersAPIUrl);
            return View(Orders);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var members = await _MemberService.GetAllAsync(MemberAPIUrl);
            ViewBag.MemberName = new SelectList(members, "Id", "CompanyName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order Order)
        {
            if (!ModelState.IsValid)
            {
                var members = await _MemberService.GetAllAsync(MemberAPIUrl);
                ViewBag.MemberName = new SelectList(members, "Id", "CompanyName", Order.MemberId);
                return View(Order);
            }


            bool isCreated = await _OrderService.CreateAsync(OrdersAPIUrl, Order);
            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating Order. Please try again.");
            return View(Order);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Order Order = await _OrderService.GetByIdAsync(OrdersAPIUrl, id);
            var members = await _MemberService.GetAllAsync(MemberAPIUrl);
            ViewBag.MemberName = new SelectList(members, "Id", "MemberId");
            if (Order == null)
            {
                return NotFound();
            }


            return View(Order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order Order)
        {
            if (!ModelState.IsValid)
            {

                return View(Order);
            }
            var members = await _MemberService.GetAllAsync(MemberAPIUrl);
            ViewBag.MemberName = new SelectList(members, "Id", "MemberId", Order.MemberId);

            bool isUpdated = await _OrderService.UpdateAsync(OrdersAPIUrl, Order, Order.Id);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error updating Order. Please try again.");
            return View(Order);
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _OrderService.DeleteAsync(OrdersAPIUrl, id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {

            Order Order = await _OrderService.GetByIdAsync(OrdersAPIUrl, id);

            if (Order == null)
            {
                return NotFound();
            }
            return View(Order);
        }

    }
}
