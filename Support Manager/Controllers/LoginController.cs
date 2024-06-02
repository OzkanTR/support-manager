using Microsoft.AspNetCore.Mvc;
using Support_Manager.DataAccess;

namespace Support_Manager.Controllers
{
    public class LoginController : Controller
    {
        private readonly SupportManagerContext _context;

        public LoginController()
        {
             _context = new SupportManagerContext();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            User kullanici = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            bool kullaniciVarMi = kullanici == null ? false : true;
            bool sifreDogruMu = kullanici.Password == user.Password ? true : false;

            if (!kullaniciVarMi) 
            {
                return View();
            }

            if (!sifreDogruMu)
            {
                return View();
            }

            HttpContext.Session.SetString("kullanici", kullanici.UserName);
            HttpContext.Session.SetString("email", kullanici.Email);
            HttpContext.Session.SetInt32("userId", kullanici.UserId);

            return RedirectToAction("Index", "Home");
        }
    }
}
