 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Support_Manager.Constants;
using Support_Manager.DataAccess;

namespace Support_Manager.Controllers
{
    public class CustomerController : Controller
    {
        private readonly SupportManagerContext _context;

        public CustomerController(SupportManagerContext context)
        {
            _context = context;
        }

        // GET: Customer
        public IActionResult Index()
        {
            var supportManagerContext = _context.Customers.Include(c => c.Follower).Include(c => c.Status);
            return View(supportManagerContext.ToList());
        }

        // GET: Customer/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers
                .Include(c => c.Follower)
                .Include(c => c.Status)
                .FirstOrDefault(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["FollowerId"] = new SelectList(_context.Followers, "FollowerUserId", "FollowerUserName");
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            customer.StatusId = EnumStatus.Aktif.GetHashCode();

            if (ModelState.IsValid)
            {
                _context.Add(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FollowerId"] = new SelectList(_context.Followers, "FollowerUserId", "FollowerUserId", customer.FollowerId);
            return View(customer);
        }

        // GET: Customer/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["FollowerId"] = new SelectList(_context.Followers, "FollowerUserId", "FollowerUserId", customer.FollowerId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", customer.StatusId);
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,AddressDetail,StatusId,CreatedDate,FollowerId")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            ViewData["FollowerId"] = new SelectList(_context.Followers, "FollowerUserId", "FollowerUserId", customer.FollowerId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", customer.StatusId);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Follower)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
