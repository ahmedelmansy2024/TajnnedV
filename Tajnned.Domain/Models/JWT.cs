using System;
using System.Collections.Generic;
using System.Text;

namespace Tajnned.Domain.Models
{
    public class JWT
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public double DurationInDays { get; set; }
    }
    public class usertest 
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

    }
}
