global using TaskManagement.DTOs.AuthDto;
global using System.Security.Claims;
global using TaskManagement.Data;
global using TaskManagement.Models;
global using Microsoft.EntityFrameworkCore;
global using TaskManagement.Services;
global using Microsoft.AspNetCore.Mvc;
global using TaskManagement.DTOs.UserDto;
global using TaskManagement.DTOs.ColumnDto;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using TaskManagement.DTOs.BoardDto;


using Microsoft.IdentityModel.Tokens;
using TaskManagement.Services.AuthService;
using TaskManagement.Services.UserService;
using TaskManagement.Services.BoardService;
using TaskManagement.Services.Helper;
using TaskManagement.Services.ColumnService;
using TaskManagement.Services.TaskService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IHelper, Helper>();
builder.Services.AddScoped<IColumnService, ColumnService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ISubTaskService, SubTaskService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Token:SecurityToken").Value!)),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
