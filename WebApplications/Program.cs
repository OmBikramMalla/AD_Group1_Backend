using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Application.Middlewares;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Helpers;
using WebApplications.Infrastructure.Presistance;
using WebApplications.Infrastructure.Services;
using WebApplications.Application.Interfaces.IRepositories;
using WebApplications.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers + OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("defaultConnection"))
);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Customer Feature Services
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Identity
builder.Services.AddIdentity<Users, Roles>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// JWT Helper
builder.Services.AddScoped<JwtHelper>();

// Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Exception Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// OpenAPI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Pipeline
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();