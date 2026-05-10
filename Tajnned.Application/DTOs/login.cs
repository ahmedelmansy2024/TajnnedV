using System;
using System.Collections.Generic;
using System.Text;

namespace Tajnned.Application.DTOs
{
    public record LoginRequest(string Username, string Password);

    public record LoginResponse
    (
         string Message,
         bool IsAuthenticated,
         string Username,
         string Email,
         List<string> Roles,
         string Token,
         DateTime? ExpiresOn);
        
    
}
