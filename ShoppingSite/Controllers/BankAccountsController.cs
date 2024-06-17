using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.Data;
using ShoppingSite.Models;

namespace ShoppingSite.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BankAccounts.Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccounts = await _context.BankAccounts
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccounts == null)
            {
                return NotFound();
            }

            return View(bankAccounts);
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchCode,AccountNum,UserId")] BankAccounts bankAccounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankAccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bankAccounts.UserId);
            return View(bankAccounts);
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccounts = await _context.BankAccounts.FindAsync(id);
            if (bankAccounts == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bankAccounts.UserId);
            return View(bankAccounts);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchCode,AccountNum,UserId")] BankAccounts bankAccounts)
        {
            if (id != bankAccounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountsExists(bankAccounts.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bankAccounts.UserId);
            return View(bankAccounts);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccounts = await _context.BankAccounts
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccounts == null)
            {
                return NotFound();
            }

            return View(bankAccounts);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankAccounts = await _context.BankAccounts.FindAsync(id);
            if (bankAccounts != null)
            {
                _context.BankAccounts.Remove(bankAccounts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountsExists(int id)
        {
            return _context.BankAccounts.Any(e => e.Id == id);
        }
    }
}
