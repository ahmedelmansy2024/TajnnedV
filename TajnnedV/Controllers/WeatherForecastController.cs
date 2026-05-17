using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TajnnedV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];
        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}


      

//if (result != null && result.IsAuthenticated)
//{
//    var claims = new List<Claim>
//    {
//        new Claim(ClaimTypes.Name, result.Username),
//        new Claim(ClaimTypes.Email, result.Email),
//        new Claim("Token", result.Token),
//        new Claim("ExpiresOn", result.ExpiresOn.ToString("O"))
//    };

//    foreach (var role in result.Roles)
//    {
//        claims.Add(new Claim(ClaimTypes.Role, role));
//    }

//    var identity = new ClaimsIdentity(
//        claims,
//        CookieAuthenticationDefaults.AuthenticationScheme);

//    var principal = new ClaimsPrincipal(identity);

//    await HttpContext.SignInAsync(
//        CookieAuthenticationDefaults.AuthenticationScheme,
//        principal);