using MedBillPro.Data;
using MedBillPro.Interfaces;
using MedBillPro.Models;
using MedBillPro.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add logging configuration early
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequiredLength = builder.Configuration.GetValue<int>("IdentityOptions:Password:RequiredLength");
    options.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("IdentityOptions:Password:RequiredUniqueChars");
    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("IdentityOptions:Password:RequireNonAlphanumeric");
    options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("IdentityOptions:Password:RequireLowercase");
    options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("IdentityOptions:Password:RequireUppercase");
    options.Password.RequireDigit = builder.Configuration.GetValue<bool>("IdentityOptions:Password:RequireDigit");

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Parse(
        builder.Configuration["IdentityOptions:Lockout:DefaultLockoutTimeSpan"]);
    options.Lockout.MaxFailedAccessAttempts = builder.Configuration.GetValue<int>(
        "IdentityOptions:Lockout:MaxFailedAccessAttempts");
    options.Lockout.AllowedForNewUsers = builder.Configuration.GetValue<bool>(
        "IdentityOptions:Lockout:AllowedForNewUsers");

    // User settings
    options.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>(
        "IdentityOptions:User:RequireUniqueEmail");
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add JWT configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "MedBillPro.Auth";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});
// Add JWT Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MedBillPro.Auth";
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false; // Set to true in production
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});

// Add authorization policies if needed
builder.Services.AddAuthorization();

// Register services
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddControllersWithViews();

// Add CORS policy if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// IMPORTANT: These must be in this exact order
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Apply pending migrations automatically (remove in production)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();