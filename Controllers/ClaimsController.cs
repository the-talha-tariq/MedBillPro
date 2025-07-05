using MedBillPro.Data;
using MedBillPro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedBillPro.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Claims
        public async Task<IActionResult> Index()
        {
            var claims = await _context.Claims
                .Include(c => c.Patient)
                .Include(c => c.Payments)
                .ToListAsync();
            return View(claims);
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims
                .Include(c => c.Patient)
                .Include(c => c.Payments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName");
            ViewData["StatusOptions"] = new SelectList(new[] { "Pending", "Processing", "Approved", "Denied" });
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimNumber,PatientId,ServiceProvider,ServiceDate,Description,Amount,Status,Notes,SubmittedDate,ProcessedDate")] MedBillPro.Models.Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.CreatedAt = DateTime.Now;
                claim.UpdatedAt = DateTime.Now;
                _context.Add(claim);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Claim created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName", claim.PatientId);
            ViewData["StatusOptions"] = new SelectList(new[] { "Pending", "Processing", "Approved", "Denied" }, claim.Status);
            return View(claim);
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName", claim.PatientId);
            ViewData["StatusOptions"] = new SelectList(new[] { "Pending", "Processing", "Approved", "Denied" }, claim.Status);
            return View(claim);
        }

        // POST: Claims/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClaimNumber,PatientId,ServiceProvider,ServiceDate,Description,Amount,Status,Notes,SubmittedDate,ProcessedDate,CreatedAt")] MedBillPro.Models.Claim claim)
        {
            if (id != claim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    claim.UpdatedAt = DateTime.Now;
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Claim updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName", claim.PatientId);
            ViewData["StatusOptions"] = new SelectList(new[] { "Pending", "Processing", "Approved", "Denied" }, claim.Status);
            return View(claim);
        }

        // GET: Claims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims
                .Include(c => c.Patient)
                .Include(c => c.Payments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Claim deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ClaimExists(int id)
        {
            return _context.Claims.Any(e => e.Id == id);
        }
    }
}