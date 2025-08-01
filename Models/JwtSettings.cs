﻿namespace MedBillPro.Models
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutes { get; set; }
        public int RefreshTokenExpireDays { get; set; }
    }
}