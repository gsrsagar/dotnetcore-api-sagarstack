using api_web_new.Application.Interfaces;
using api_web_new.Application.Services;
using api_web_new.csproj.Domain.DTO.Request;
using api_web_new.csproj.Domain.Entities;
using api_web_new.csproj.Domain.Validators;
using api_web_new.Infrastructure;
using api_web_new.Infrastructure.Repositories;
using api_web_new.Services;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmployeeService<Employee,EmployeeRequestDTO>, EmployeeService>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<IEmployeeValidator, EmployeeValidator>();
builder.Services.AddScoped<DbContext, AppDbContext>();

var connectionString =
    builder.Configuration.GetConnectionString("MySqlConnection") ??
    throw new InvalidOperationException("Connection string 'MySqlConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString, mySqlOptions =>
    {
        mySqlOptions.MigrationsAssembly("api-web-new.Infrastructure");
    });
});

// till DI

var app = builder.Build();

// Configure the HTTP request pipeline. Middlewares in Rewuest pipeline
if (app.Environment.IsDevelopment())
{
   // app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
