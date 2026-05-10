using Autofac.Core;
using Hangfire;
using Hangfire.Logging;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;
using Tajnned.Api.Middlewares;
using Tajnned.Domain.Models;
using Tajnned.Infrastructure.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
//Hangfire Setup
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddHangfireServer();


//builder.Services.AddMediatR(cfg =>
//    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        Assembly.Load("Tajnned.Application")   //  
    ));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
               .AddJwtBearer(o =>
               {
                   o.RequireHttpsMetadata = false;
                   o.SaveToken = false;
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidIssuer = builder.Configuration["JWT:Issuer"],
                       ValidAudience = builder.Configuration["JWT:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                   };
               });
builder.Services.AddSwaggerGen(c => {
c.SwaggerDoc("v1", 
    new Microsoft.OpenApi.OpenApiInfo { Title = "My API", Version = "v1" }); 
 
  });
//   Serilog Setup
//Log.Logger = new LoggerConfiguration()
//   .MinimumLevel.Information()
//   .WriteTo.Console()
//   .WriteTo.File(
//       "Logs/app-.txt",
//       rollingInterval: RollingInterval.Day)
//   .Enrich.FromLogContext()
//   .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
     app.MapOpenApi();
     //app.UseMiddleware<ExceptionHandlingMiddleware>();

}
app.UseHangfireDashboard("/hangfire");
app.UseHttpsRedirection();
//app.UseCors(options =>
//{
//    options.AllowAnyMethod()
//           .AllowAnyHeader()
//           .AllowAnyOrigin()
//           .WithExposedHeaders("Content-Disposition");
//});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
