using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PostgresCrud.Application.Interfaces.Repositories;
using PostgresCrud.Application.Interfaces.Security;
using PostgresCrud.Application.Services.Users;
using PostgresCrud.Data;
using PostgresCrud.Infrastructure.Repositories.Users;
using PostgresCrud.Infrastructure.Security;
using PostgresCrud.Infrastructure.Settings;
using PostgresCrud.Mappings;
using PostgresCrud.Repositories;
using PostgresCrud.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Options pattern para JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Security
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Controllers
builder.Services.AddControllers();

// JWT Authentication
var jwt = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,  ValidIssuer   = jwt["Issuer"],
        ValidateAudience         = true,  ValidAudience = jwt["Audience"],
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt["Secret"]!)),
    });

builder.Services.AddAuthorizationBuilder();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();