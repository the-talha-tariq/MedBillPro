using MedBillPro.Data;
using MedBillPro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedBillPro.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Calculate total revenue from payments
            var payments = await _context.Payments.ToListAsync();
            var totalRevenue = payments.Sum(p => p.Amount);

            var dashboardData = new DashboardViewModel
            {
                TotalPatients = await _context.Patients.CountAsync(),
                TotalClaims = await _context.Claims.CountAsync(),
                TotalPayments = await _context.Payments.CountAsync(),
                TotalRevenue = totalRevenue,
                PendingClaims = await _context.Claims.CountAsync(c => c.Status == "Pending"),
                ApprovedClaims = await _context.Claims.CountAsync(c => c.Status == "Approved"),
                DeniedClaims = await _context.Claims.CountAsync(c => c.Status == "Denied"),
                ProcessingClaims = await _context.Claims.CountAsync(c => c.Status == "Processing"),
                RecentClaims = await _context.Claims
                    .Include(c => c.Patient)
                    .OrderByDescending(c => c.CreatedAt)
                    .Take(5)
                    .ToListAsync(),
                RecentPayments = await _context.Payments
                    .Include(p => p.Claim)
                    .ThenInclude(c => c.Patient)
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(5)
                    .ToListAsync()
            };

            return View(dashboardData);
        }


    }

    public class DashboardViewModel
    {
        public int TotalPatients { get; set; }
        public int TotalClaims { get; set; }
        public int TotalPayments { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PendingClaims { get; set; }
        public int ApprovedClaims { get; set; }
        public int DeniedClaims { get; set; }
        public int ProcessingClaims { get; set; }
        public List<Claim> RecentClaims { get; set; } = new();
        public List<Payment> RecentPayments { get; set; } = new();
    }
}