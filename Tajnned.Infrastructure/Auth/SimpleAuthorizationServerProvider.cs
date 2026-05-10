//using Microsoft.Owin.Security.OAuth;
//using System;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Text;

//namespace Tajnned.Infrastructure.Auth
//{
// public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
//{
//    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
//    {
//        context.Validated();
//    }

//    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
//    {
//        if (context.UserName != "admin" || context.Password != "123")
//        {
//            context.SetError("invalid_grant", "Invalid credentials");
//            return;
//        }

//        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
//        identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

//        context.Validated(identity);
//    }
//}
//}
