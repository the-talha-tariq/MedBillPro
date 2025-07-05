using Microsoft.AspNetCore.Identity;

namespace MedBillPro.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add custom properties here
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}