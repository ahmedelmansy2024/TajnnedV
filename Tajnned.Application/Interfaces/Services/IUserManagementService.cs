using System;
using System.Collections.Generic;
using System.Text;
using Tajnned.Application.DTOs;

namespace Tajnned.Application.Interfaces.Services
{
    public interface IUserManagementService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    }

}
