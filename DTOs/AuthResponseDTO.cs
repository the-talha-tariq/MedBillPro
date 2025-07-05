using Azure;

namespace MedBillPro.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpires { get; set; }
        public UserDetailsDTO UserDetails { get; set; }
    }
}

