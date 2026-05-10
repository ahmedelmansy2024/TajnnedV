using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tajnned.Application.DTOs;
using Tajnned.Application.Interfaces.Services;
using Tajnned.Application.Interfaces.Sql;
using Tajnned.Domain.Interfaces.Repositories;
using Tajnned.Domain.Models;
using Tajnned.Infrastructure.Data.Entities;

namespace Tajnned.Infrastructure.Auth
{
    public class UserManagementService : IUserManagementService
    {

        private readonly IRepository<User> _repository;
        private readonly JWT _jwt;
        private readonly ISqlExecutor _sql;
        private readonly ILogger<UserManagementService> _logger;

        public UserManagementService(ILogger<UserManagementService> logger,IRepository<User> repository, IOptions<JWT> jwt, ISqlExecutor sql)
        {
            _repository = repository;
            _jwt = jwt.Value;
            _sql = sql;
            _logger= logger;    
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Creating User");

            try
            {
                var userrr = await _sql.QuerySingleAsync<usertest>(
          "EXEC sp_Logintest @Username, @Password",

          new SqlParameter("@Username", request.Username),
          new SqlParameter("@Password", request.Password)
      );

                var userx = (await _repository.ExecuteSqlAsync(
              "EXEC sp_Login @Username, @Password",
              new SqlParameter("@Username", request.Username),
              new SqlParameter("@Password", request.Password)
          )).FirstOrDefault();
                var user = _repository.GetAll().Where(e => e.Name == request.Username).FirstOrDefault();
                if (user is null)
                    return new LoginResponse("Username or Password is incorrect!", false, "", "", null, "", null);

                var jwtSecurityToken = await CreateJwtToken(user);
                var Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                //var rolesList = await _userManager.GetRolesAsync(user);
                return new LoginResponse("true",
                  true, user.Name, user.Email, null, Token
                 , jwtSecurityToken.ValidTo);



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Creating User");

                throw;
            }


           

            
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            //var userClaims = await _userManager.GetClaimsAsync(user);
            //var roles = await _userManager.GetRolesAsync(user);
            //var roleClaims = new List<Claim>();

            //foreach (var role in roles)
            //    roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim( ClaimTypes.Name , user.Name)
            };
            // .Union(userClaims)
            // .Union(roleClaims);

            //var key = new SymmetricSecurityKey(
            //    Encoding.UTF8.GetBytes(jwt["Key"]));

            //var token = new JwtSecurityToken(
            //    issuer: jwt["Issuer"],
            //    audience: jwt["Audience"],
            //    claims: claims,
            //    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwt["DurationInMinutes"])),
            //    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            //);

            //return new JwtSecurityTokenHandler().WriteToken(token);



            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
