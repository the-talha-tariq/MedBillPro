using MedBillPro.Models;
using System.Security.Claims;

namespace MedBillPro.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        int GetTokenExpirationTimeInMinutes();
    }

}
