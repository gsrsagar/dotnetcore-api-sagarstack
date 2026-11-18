using api_web_new.Application.Interfaces;
using api_web_new.Application.Services;
using api_web_new.csproj.Domain.DTO.Request;
using api_web_new.csproj.Domain.Entities;
using api_web_new.csproj.Domain.Validators;
using api_web_new.Infrastructure;
using api_web_new.Infrastructure.Repositories;
using api_web_new.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using MySql.EntityFrameworkCore.Extensions;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using NLog;
using NLog.Web;
using api_web_new.Middlewares;


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

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My API",
        Description = "My API Description"
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "My API v2",
        Description = "My API Description v2"
    });
});

builder.Logging.ClearProviders();
builder.Host.UseNLog();

// till DI

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.RoutePrefix = string.Empty; // Root URL
    options.DocumentTitle = "My Versioned Web API";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
});

app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

app.UseMiddleware<GlobalExceptionHandlerMiddleware>(); // custom middleware
// Configure the HTTP request pipeline. Middlewares in Rewuest pipeline
if (app.Environment.IsDevelopment())
{
   // app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
