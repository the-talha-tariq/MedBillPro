using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedBillPro.Data;
using MedBillPro.Models;
using Microsoft.AspNetCore.Authorization;

namespace MedBillPro.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = await _context.Payments
                .Include(p => p.Claim)
                .ThenInclude(c => c.Patient)
                .ToListAsync();
            return View(payments);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Claim)
                .ThenInclude(c => c.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            var claims = _context.Claims
                .Include(c => c.Patient)
                .Select(c => new { c.Id, DisplayText = $"{c.ClaimNumber} - {c.Patient.FullName}" })
                .ToList();

            ViewData["ClaimId"] = new SelectList(claims, "Id", "DisplayText");
            ViewData["PaymentMethodOptions"] = new SelectList(new[] { "Insurance", "Patient", "Other" });
            ViewData["StatusOptions"] = new SelectList(new[] { "Pending", "Completed", "Failed" });
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimId,Amount,PaymentDate,PaymentMethod,PayerName,ReferenceNumber,Status,Notes")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.CreatedAt = DateTime.Now;
                payment.UpdatedAt = DateTime.Now;
                _context.Add(payment);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Payment recorded successfully!";
                return RedirectToAction(nameof(Index));
            }

            var claims = _context.Claims
                .Include(c => c.Patient)
                .Select(c => new { c.Id, DisplayText = $"{c.ClaimNumber} - {c.Patient.FullName}" })
                .ToList();

            ViewData["ClaimId"] = new SelectList(claims, "Id", "DisplayText", payment.ClaimId);
            ViewData["PaymentMethodOptions"] = new SelectList(new[] { "Insurance", "Patient", "Other" }, payment.PaymentMethod);
            ViewData["StatusOptions"] = new SelectList(new[] { "Pending", "Completed", "Failed" }, payment.Status);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            var claims = _context.Claims
                .Include(c => c.Patient)
                .Select(c => new { c.Id, DisplayText = $"{c.ClaimNumber} - {c.Patient.FullName}" })
                .ToList();

            ViewData["ClaimId"] = new SelectList(claims, "Id", "DisplayText", payment.ClaimId);
            ViewData["PaymentMethodOptions"] = new SelectList(new[] { "Insurance", "Patient", "Other" }, payment.PaymentMethod);
            ViewData["StatusOptions"] = new SelectList(new[] { "Pending", "Completed", "Failed" }, payment.Status);
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClaimId,Amount,PaymentDate,PaymentMethod,PayerName,ReferenceNumber,Status,Notes,CreatedAt")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    payment.UpdatedAt = DateTime.Now;
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Payment updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
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

            var claims = _context.Claims
                .Include(c => c.Patient)
                .Select(c => new { c.Id, DisplayText = $"{c.ClaimNumber} - {c.Patient.FullName}" })
                .ToList();

            ViewData["ClaimId"] = new SelectList(claims, "Id", "DisplayText", payment.ClaimId);
            ViewData["PaymentMethodOptions"] = new SelectList(new[] { "Insurance", "Patient", "Other" }, payment.PaymentMethod);
            ViewData["StatusOptions"] = new SelectList(new[] { "Pending", "Completed", "Failed" }, payment.Status);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Claim)
                .ThenInclude(c => c.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Payment deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}