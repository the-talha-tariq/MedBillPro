using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedBillPro.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ClaimNumber { get; set; } = string.Empty;

        [Required]
        public int PatientId { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceProvider { get; set; } = string.Empty;

        [Required]
        public DateTime ServiceDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Denied, Processing

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        public DateTime SubmittedDate { get; set; }
        public DateTime? ProcessedDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPaid => Payments.Sum(p => p.Amount);

        [Column(TypeName = "decimal(18,2)")]
        public decimal RemainingBalance => Amount - TotalPaid;
    }
}