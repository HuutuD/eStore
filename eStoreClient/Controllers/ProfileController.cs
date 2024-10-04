using BussinessObject;
using eStoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eStoreClient.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApiService<Member> _memberRepository;
        private readonly string _membersAPIUrl;

        public ProfileController(ApiService<Member> memberRepository, IOptions<ApiUrls> apiUrls)
        {
            _memberRepository = memberRepository;
            _membersAPIUrl = apiUrls.Value.MembersAPIUrl;
        }

        // GET: /Profile/Edit
        public async Task<IActionResult> Edit()
        {
            var userName = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Index", "Logins");
            }

            // Fetch the user profile from the repository
            List<Member> members = await _memberRepository.GetAllAsync(_membersAPIUrl);
            var user = members.FirstOrDefault(m => m.Email == userName);

            if (user == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                Id = user.Id,
                UserName = user.CompanyName,
                Email = user.Email,
                CompanyName = user.CompanyName,
                City = user.City,
                Country = user.Country
        
            };

            return View(model);
        }

        // POST: /Profile/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
    

            var userName = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Index", "Logins");
            }

            // Fetch the user profile from the repository
            List<Member> members = await _memberRepository.GetAllAsync(_membersAPIUrl);
            var user = members.FirstOrDefault(m => m.Email == userName);

            if (user == null)
            {
                return NotFound();
            }

            // Update user profile
            user.Email = model.Email;
            user.CompanyName = model.CompanyName;
            user.City = model.City;
            user.Country = model.Country;         

            await _memberRepository.UpdateAsync(_membersAPIUrl,user,user.Id);

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Edit");
        }
    }
}
