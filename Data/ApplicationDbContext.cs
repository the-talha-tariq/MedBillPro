using MedBillPro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedBillPro.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Patient entity
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configure Claim entity
            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasPrecision(18, 2);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasIndex(e => e.ClaimNumber).IsUnique();

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Payment entity
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasPrecision(18, 2);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@email.com",
                    Phone = "(555) 123-4567",
                    DateOfBirth = new DateTime(1980, 5, 15),
                    Address = "123 Main St",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10001",
                    InsuranceProvider = "Blue Cross Blue Shield",
                    InsurancePolicyNumber = "BC123456789",
                    InsuranceGroupNumber = "GRP001",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Patient
                {
                    Id = 2,
                    FirstName = "Sarah",
                    LastName = "Johnson",
                    Email = "sarah.johnson@email.com",
                    Phone = "(555) 987-6543",
                    DateOfBirth = new DateTime(1975, 8, 22),
                    Address = "456 Oak Ave",
                    City = "Los Angeles",
                    State = "CA",
                    ZipCode = "90210",
                    InsuranceProvider = "Aetna",
                    InsurancePolicyNumber = "AET987654321",
                    InsuranceGroupNumber = "GRP002",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Patient
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Brown",
                    Email = "michael.brown@email.com",
                    Phone = "(555) 555-0123",
                    DateOfBirth = new DateTime(1990, 12, 3),
                    Address = "789 Pine Blvd",
                    City = "Chicago",
                    State = "IL",
                    ZipCode = "60601",
                    InsuranceProvider = "Cigna",
                    InsurancePolicyNumber = "CIG456789123",
                    InsuranceGroupNumber = "GRP003",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            // Seed Claims
            modelBuilder.Entity<Claim>().HasData(
                new Claim
                {
                    Id = 1,
                    ClaimNumber = "CLM001",
                    PatientId = 1,
                    ServiceProvider = "City Medical Center",
                    ServiceDate = new DateTime(2024, 1, 15),
                    Description = "Annual Physical Examination",
                    Amount = 350.00m,
                    Status = "Approved",
                    Notes = "Routine checkup completed",
                    SubmittedDate = new DateTime(2024, 1, 16),
                    ProcessedDate = new DateTime(2024, 1, 20),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Claim
                {
                    Id = 2,
                    ClaimNumber = "CLM002",
                    PatientId = 2,
                    ServiceProvider = "West Coast Specialists",
                    ServiceDate = new DateTime(2024, 2, 1),
                    Description = "Cardiology Consultation",
                    Amount = 450.00m,
                    Status = "Processing",
                    Notes = "Awaiting additional documentation",
                    SubmittedDate = new DateTime(2024, 2, 2),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Claim
                {
                    Id = 3,
                    ClaimNumber = "CLM003",
                    PatientId = 1,
                    ServiceProvider = "Downtown Radiology",
                    ServiceDate = new DateTime(2024, 2, 10),
                    Description = "MRI Scan",
                    Amount = 1200.00m,
                    Status = "Pending",
                    Notes = "Prior authorization required",
                    SubmittedDate = new DateTime(2024, 2, 11),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Claim
                {
                    Id = 4,
                    ClaimNumber = "CLM004",
                    PatientId = 3,
                    ServiceProvider = "Emergency Care Center",
                    ServiceDate = new DateTime(2024, 2, 20),
                    Description = "Emergency Room Visit",
                    Amount = 850.00m,
                    Status = "Denied",
                    Notes = "Service not covered under current plan",
                    SubmittedDate = new DateTime(2024, 2, 21),
                    ProcessedDate = new DateTime(2024, 2, 25),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            // Seed Payments
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    ClaimId = 1,
                    Amount = 280.00m,
                    PaymentDate = new DateTime(2024, 1, 25),
                    PaymentMethod = "Insurance",
                    PayerName = "Blue Cross Blue Shield",
                    ReferenceNumber = "PAY001",
                    Status = "Completed",
                    Notes = "80% coverage payment",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Payment
                {
                    Id = 2,
                    ClaimId = 1,
                    Amount = 70.00m,
                    PaymentDate = new DateTime(2024, 1, 30),
                    PaymentMethod = "Patient",
                    PayerName = "John Smith",
                    ReferenceNumber = "PAY002",
                    Status = "Completed",
                    Notes = "Patient copay",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Payment
                {
                    Id = 3,
                    ClaimId = 2,
                    Amount = 360.00m,
                    PaymentDate = new DateTime(2024, 2, 15),
                    PaymentMethod = "Insurance",
                    PayerName = "Aetna",
                    ReferenceNumber = "PAY003",
                    Status = "Completed",
                    Notes = "Partial payment received",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
        }
    }
}