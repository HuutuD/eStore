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
    public class MembersController : Controller
    {

        private readonly ApiService<Member> _MemberService;
        private readonly string MembersAPIUrl;

        public MembersController(ApiService<Member> MemberService,
                        IOptions<ApiUrls> apiUrls)
        {
            _MemberService = MemberService;
            MembersAPIUrl = apiUrls.Value.MembersAPIUrl;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Member> members = await _MemberService.GetAllAsync(MembersAPIUrl);
            return View(members);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Member Member)
        {
            if (!ModelState.IsValid)
            {
                return View(Member);
            }

            bool isCreated = await _MemberService.CreateAsync(MembersAPIUrl, Member);
            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating Member. Please try again.");
            return View(Member);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Member Member = await _MemberService.GetByIdAsync(MembersAPIUrl, id);
            if (Member == null)
            {
                return NotFound();
            }


            return View(Member);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Member Member)
        {
            if (!ModelState.IsValid)
            {

                return View(Member);
            }

            bool isUpdated = await _MemberService.UpdateAsync(MembersAPIUrl, Member, Member.Id);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error updating Member. Please try again.");
            return View(Member);
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _MemberService.DeleteAsync(MembersAPIUrl, id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {

            Member Member = await _MemberService.GetByIdAsync(MembersAPIUrl, id);

            if (Member == null)
            {
                return NotFound();
            }
            return View(Member);
        }

    }
}
