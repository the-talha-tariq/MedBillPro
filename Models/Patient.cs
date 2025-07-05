using System.ComponentModel.DataAnnotations;

namespace MedBillPro.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string InsuranceProvider { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string InsurancePolicyNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string InsuranceGroupNumber { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

        public string FullName => $"{FirstName} {LastName}";
    }
}