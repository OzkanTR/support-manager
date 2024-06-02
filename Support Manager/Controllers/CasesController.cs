using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Support_Manager.Constants;
using Support_Manager.DataAccess;
using Support_Manager.Models;

namespace Support_Manager.Controllers
{
    public class CasesController : Controller
    {
        private readonly SupportManagerContext _context;

        public CasesController(SupportManagerContext context)
        {
            _context = context;
        }

        // GET: Cases
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
                CasesModel model = new CasesModel
                {
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
                                          .Include(c => c.Customer).ToList()
                };
                return View(model);
            }
        }

        // GET: Cases/Details/5
        public IActionResult Details(int? id)
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
                    Cases = _context.Cases.Where(m => m.CaseId == id)
                                .Include(c => c.CreatedUser)
                                .Include(c => c.Customer)
                                .Include(c => c.Follower)
                                .Include(c => c.Program)
                                .Include(c => c.Status)
                                .Include(c => c.UpdateUser)
                                .Include(c => c.User).ToList()
                };
                return View(model);
            }
        }

        // GET: Cases/Create
        public IActionResult Create()
        {
            ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "ProgramName");
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Case @case)
        {
            
            @case.StatusId = EnumStatus.Incelemede.GetHashCode();
            // Case CreatedUser, Follower, User bilgilerinin sessiondan alınıp modelde doldurulması sağlanacak.
            _context.Add(@case);
            _context.SaveChanges();

            ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "ProgramName");
            return View();
        }

        // GET: Cases/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case =  _context.Cases.Find(id);
            if (@case == null)
            {
                return NotFound();
            }
            ViewData["FollowerId"] = new SelectList(_context.Followers, "FollowerUserId", "FollowerUserId", @case.Follower.FollowerUserName);
            ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "ProgramId", @case.Program.ProgramName);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", @case.Status.StatusName);
            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CaseId,CaseNumber,CaseName,CaseMessage,FollowerId,CustomerId,UserId,StatusId,CreatedUserId,CreateDate,UpdateUserId,UpdateDate,ProgramId")] Case @case)
        {
            if (id != @case.CaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@case);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(@case.CaseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", @case.CustomerId);
            ViewData["FollowerId"] = new SelectList(_context.Followers, "FollowerUserId", "FollowerUserId", @case.Follower.FollowerUserName);
            ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "ProgramId", @case.Program.ProgramName);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", @case.Status.StatusName);
            return View(@case);
        }

        // GET: Cases/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = _context.Cases
                .Include(c => c.CreatedUser)
                .Include(c => c.Customer)
                .Include(c => c.Follower)
                .Include(c => c.Program)
                .Include(c => c.Status)
                .Include(c => c.UpdateUser)
                .Include(c => c.User)
                .FirstOrDefault(m => m.CaseId == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var @case = _context.Cases.Find(id);
            if (@case != null)
            {
                _context.Cases.Remove(@case);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseExists(int id)
        {
            return _context.Cases.Any(e => e.CaseId == id);
        }
    }
}
