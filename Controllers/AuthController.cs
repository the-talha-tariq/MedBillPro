using MedBillPro.DTOs;
using MedBillPro.Interfaces;
using MedBillPro.Models;
using MedBillPro.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MedBillPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = new ApplicationUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                // Add user to default role (optional)
                await _userManager.AddToRoleAsync(user, "User");

                return Ok(new { Message = "Registration successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    return Unauthorized("Invalid email or password");
                }

                var token = _jwtService.GenerateToken(user);
                var refreshToken = _jwtService.GenerateRefreshToken();
                var roles = await _userManager.GetRolesAsync(user);

                // In a real app, you'd save the refresh token to the database
                // with an expiration date and link it to the user

                return Ok(new AuthResponseDTO
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    TokenExpires = DateTime.Now.AddMinutes(_jwtService.GetTokenExpirationTimeInMinutes()),
                    UserDetails = new UserDetailsDTO
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Roles = roles
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}