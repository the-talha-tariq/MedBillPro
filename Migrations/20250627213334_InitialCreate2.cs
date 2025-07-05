using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedBillPro.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InsuranceProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsurancePolicyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsuranceGroupNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ServiceProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PayerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "City", "CreatedAt", "DateOfBirth", "Email", "FirstName", "InsuranceGroupNumber", "InsurancePolicyNumber", "InsuranceProvider", "LastName", "Phone", "State", "UpdatedAt", "ZipCode" },
                values: new object[,]
                {
                    { 1, "123 Main St", "New York", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2421), new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@email.com", "John", "GRP001", "BC123456789", "Blue Cross Blue Shield", "Smith", "(555) 123-4567", "NY", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2449), "10001" },
                    { 2, "456 Oak Ave", "Los Angeles", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2458), new DateTime(1975, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "sarah.johnson@email.com", "Sarah", "GRP002", "AET987654321", "Aetna", "Johnson", "(555) 987-6543", "CA", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2459), "90210" },
                    { 3, "789 Pine Blvd", "Chicago", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2463), new DateTime(1990, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.brown@email.com", "Michael", "GRP003", "CIG456789123", "Cigna", "Brown", "(555) 555-0123", "IL", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2464), "60601" }
                });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "Amount", "ClaimNumber", "CreatedAt", "Description", "Notes", "PatientId", "ProcessedDate", "ServiceDate", "ServiceProvider", "Status", "SubmittedDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 350.00m, "CLM001", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2829), "Annual Physical Examination", "Routine checkup completed", 1, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "City Medical Center", "Approved", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2831) },
                    { 2, 450.00m, "CLM002", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2837), "Cardiology Consultation", "Awaiting additional documentation", 2, null, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "West Coast Specialists", "Processing", new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2838) },
                    { 3, 1200.00m, "CLM003", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2842), "MRI Scan", "Prior authorization required", 1, null, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Downtown Radiology", "Pending", new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2843) },
                    { 4, 850.00m, "CLM004", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2847), "Emergency Room Visit", "Service not covered under current plan", 3, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emergency Care Center", "Denied", new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2848) }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "ClaimId", "CreatedAt", "Notes", "PayerName", "PaymentDate", "PaymentMethod", "ReferenceNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 280.00m, 1, new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2881), "80% coverage payment", "Blue Cross Blue Shield", new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Insurance", "PAY001", "Completed", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2882) },
                    { 2, 70.00m, 1, new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2887), "Patient copay", "John Smith", new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient", "PAY002", "Completed", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2887) },
                    { 3, 360.00m, 2, new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2891), "Partial payment received", "Aetna", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Insurance", "PAY003", "Completed", new DateTime(2025, 6, 28, 2, 33, 33, 275, DateTimeKind.Local).AddTicks(2891) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimNumber",
                table: "Claims",
                column: "ClaimNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PatientId",
                table: "Claims",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClaimId",
                table: "Payments",
                column: "ClaimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }
    }
}
