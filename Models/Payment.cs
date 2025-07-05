using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedBillPro.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int ClaimId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty; // Insurance, Patient, Other

        [Required]
        [StringLength(100)]
        public string PayerName { get; set; } = string.Empty;

        [StringLength(50)]
        public string ReferenceNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Completed"; // Pending, Completed, Failed

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("ClaimId")]
        public virtual Claim Claim { get; set; } = null!;
    }
}