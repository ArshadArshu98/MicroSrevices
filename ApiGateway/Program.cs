using JWTAuthentication;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("ocelot.json")
                            .Build();
builder.Services.AddOcelot(configuration);
builder.Services.AddCustomJwtAuthentication();


var app = builder.Build();
await app.UseOcelot();
app.UseAuthentication();
app.Run();
