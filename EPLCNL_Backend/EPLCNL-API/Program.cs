using BusinessObject.UnitOfWork;
using Data.Models;
using Data.UnitOfWork;
using Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Read the connection string from appsettings.json
string connectionString = configuration.GetConnectionString("MyCnn");

// Add DbContext using the connection string
builder.Services.AddDbContext<EPLCNLContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add other services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddAutoMapper(typeof(ApplicationMapper));
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(p =>
            p.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
});
builder.Services.AddEndpointsApiExplorer();

// Config JWT for swagger
builder.Services.ConfigureSwaggerGen(c =>
{
    // ... your existing swagger configuration
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Authentication & Authorization
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
