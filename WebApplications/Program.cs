using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplications.Application.Interfaces.IServices;
using WebApplications.Application.Middlewares;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Helpers;
using WebApplications.Infrastructure.Presistance;
using WebApplications.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers + OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("defaultConnection"))
);

// Customer Feature Services
builder.Services.AddScoped<ICustomerService, CustomerService>();


builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddScoped<IPartRequestRepository, PartRequestRepository>();
builder.Services.AddScoped<IPartRequestService, PartRequestService>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddScoped<ICustomerHistoryRepository, CustomerHistoryRepository>();
builder.Services.AddScoped<ICustomerHistoryService, CustomerHistoryService>();

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IVendorService, VendorService>();

builder.Services.AddScoped<ISalesInvoiceRepository, SalesInvoiceRepository>();
builder.Services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();

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