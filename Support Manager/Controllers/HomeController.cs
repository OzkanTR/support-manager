using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NuGet.Protocol.Plugins;
using Support_Manager.Constants;
using Support_Manager.DataAccess;
using Support_Manager.Models;
using System.Diagnostics;

/* Scaffold-DbContext "Server=localhost;Database=Support_Manager;Integrated Security=True;Encrypt=False;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataAccess -force */

namespace Support_Manager.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly SupportManagerContext _context;

		public HomeController(ILogger<HomeController> logger, SupportManagerContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			bool userVarMi = HttpContext.Session.GetInt32("userId").HasValue;
            if (!userVarMi)
            {
                return View();
            }
            else
            {
				var userId = HttpContext.Session.GetInt32("userId").Value;
				CasesModel model = new CasesModel {
					User = _context.Users.FirstOrDefault(u => u.UserId == userId),
					Cases = _context.Cases.Where(c => c.UserId == userId 
                                                    && (c.StatusId == EnumStatus.Incelemede.GetHashCode() 
                                                            || c.StatusId == EnumStatus.CevapBekleniyor.GetHashCode() 
                                                            || c.StatusId == EnumStatus.YazilimGelistirme.GetHashCode() 
                                                            || c.StatusId == EnumStatus.Guncellemede.GetHashCode()
                                                 ))
                                          .Include(c => c.CreatedUser)
                                          .Include(c => c.User)
                                          .Include(c => c.Follower)
                                          .Include(c => c.Status)
                                          .Include(c => c.UpdateUser)
                                          .Include(c => c.Program)
                                          .Include(c=>c.Customer).ToList()
				};
				return View(model);
			}
        }

        public IActionResult TheEnding()
        {
            bool userVarMi = HttpContext.Session.GetInt32("userId").HasValue;
            if (!userVarMi)
            {
                return View();
            }
            else
            {
                var userId = HttpContext.Session.GetInt32("userId").Value;
                CasesModel model = new CasesModel
                {
                    User = _context.Users.FirstOrDefault(u => u.UserId == userId),
                    Cases = _context.Cases.Where(c => c.UserId == userId 
                                                && (c.StatusId == EnumStatus.Tamamlandi.GetHashCode() 
                                                        || c.StatusId == EnumStatus.Reddedildi.GetHashCode() 
                                                        || c.StatusId == EnumStatus.AskiyaAlindi.GetHashCode()
                                                    ))
                                         .Include(c => c.CreatedUser)
                                         .Include(c => c.User)
                                         .Include(c => c.Follower)
                                         .Include(c => c.Status)
                                         .Include(c => c.UpdateUser)
                                         .Include(c => c.Program)
                                         .Include(c => c.Customer).ToList()
                };
                return View(model);
            }
        }

        public IActionResult AllCase()
        {
            bool userVarMi = HttpContext.Session.GetInt32("userId").HasValue;
            if (!userVarMi)
            {
                return View();
            }
            else
            {
                var userId = HttpContext.Session.GetInt32("userId").Value;
                CasesModel model = new CasesModel
                {
                    User = _context.Users.FirstOrDefault(u => u.UserId == userId),
                    Cases = _context.Cases.Include(c => c.User)
                                          .Include(c => c.Follower)
                                          .Include(c => c.Status)
                                          .Include(c => c.UpdateUser)
                                          .Include(c => c.Program)
                                          .Include(c => c.Customer).ToList()
                };
                return View(model);
            }
        }

        public IActionResult CaseDetails(int id)
        {
            bool userVarMi = HttpContext.Session.GetInt32("userId").HasValue;
            if (!userVarMi)
            {
                return View();
            }
            else
            {
                var userId = HttpContext.Session.GetInt32("userId").Value;
                CasesModel model = new CasesModel
                {
                    User = _context.Users.FirstOrDefault(u => u.UserId == userId),
                    Cases = _context.Cases.Where(c => c.CaseId == id)
                                          .Include(c => c.User)
                                          .Include(c => c.Follower)
                                          .Include(c => c.Status)
                                          .Include(c => c.UpdateUser)
                                          .Include(c => c.Program)
                                          .Include(c => c.Customer).ToList()
                };
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
