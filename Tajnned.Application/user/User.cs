using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Tajnned.Application.DTOs;
using Tajnned.Application.Interfaces.Services;

namespace Tajnned.Application.user
{
    public record GetTokenRequest( string Username, string Password) : IRequest<LoginResponse>;
    public class GetTokenHandler : IRequestHandler<GetTokenRequest, LoginResponse>
    {
        public const string ROUTE = "api/V1/User";
        private readonly IUserManagementService _userManagementService;


        public GetTokenHandler(IUserManagementService userManagementService  )
        {
                _userManagementService  = userManagementService;
        }

        public async Task<LoginResponse> Handle(GetTokenRequest request, CancellationToken cancellationToken)
        {
            var res = await _userManagementService.LoginAsync(new LoginRequest(request.Username, request.Password)  , cancellationToken);
         //   if (res == null) throw ex
            return res;
        }
    }
}
