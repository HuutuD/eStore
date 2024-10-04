using BussinessObject;
using eStoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository.IRepository;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    public class LoginsController : Controller
    {
        private readonly ApiService<Member> _memberRepository;
        private readonly string _membersAPIUrl;
        private readonly IConfiguration _configuration;

        public LoginsController(ApiService<Member> memberRepository,
                                IOptions<ApiUrls> apiUrls,
                                IConfiguration configuration)
        {
            _memberRepository = memberRepository;
            _membersAPIUrl = apiUrls.Value.MembersAPIUrl;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var defaultUserName = _configuration["Login:user"];
            var defaultPassword = _configuration["Login:pass"];

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (UserName == defaultUserName && Password == defaultPassword) 
            {
                HttpContext.Session.SetString("UserName", "Admin");
                HttpContext.Session.SetString("Type", "0");
                return Redirect("/");
            }

            List<Member> members = await _memberRepository.GetAllAsync(_membersAPIUrl);

            var user = members.FirstOrDefault(m => m.Email == UserName && m.PassWord == Password);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid login attempt";
                return View();
            }

            HttpContext.Session.SetString("UserName", UserName);
            HttpContext.Session.SetString("Type", "1");
            return Redirect("/");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
