using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using soft.Models;
using soft.Repos;
using System.Diagnostics;

namespace soft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> Role_m;
        private readonly IRepo _user;

        public HomeController(ILogger<HomeController> logger,RoleManager<IdentityRole> _Rolem,IRepo User)
        {
            _logger = logger;
            Role_m = _Rolem;
            _user = User;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register stData)
        {
            if (ModelState.IsValid)
            {
               var res=await  _user.Register(stData);
                if (!res.Succeeded)
                {
                    
                    foreach(var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(stData);
                }
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login UserD)
        {
            if (ModelState.IsValid)
            {
               var res=await  _user.Login(UserD);
                if (res.Succeeded)
                {
                    if (User.IsInRole(Roles.Admin))
                    {
                        return RedirectToAction("AdminDashboard");
                    }else if (User.IsInRole(Roles.Teacher))
                    {
                        return RedirectToAction("TeacherDashboard");
                    }
                    return RedirectToAction("StudentDashboard");
                }
                else
                {
                    ModelState.AddModelError("", "please Enter Valid Credentials");
                    return View(UserD);
                }
                
            }
            return View();
        }

        public IActionResult Logout()
        {
            _user.Logout();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult TeacherDashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult StudentDashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


       /* public string Addroles()
        {
            Role_m.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
            Role_m.CreateAsync(new IdentityRole(Roles.Teacher)).GetAwaiter().GetResult();
            Role_m.CreateAsync(new IdentityRole(Roles.Student)).GetAwaiter().GetResult();
            return "roles added";
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
