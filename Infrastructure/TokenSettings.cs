using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class TokenSettings
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int DurationInDays { get; set; } = 7;
    }
}
