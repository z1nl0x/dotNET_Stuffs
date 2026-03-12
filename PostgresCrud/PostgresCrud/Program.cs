using Microsoft.EntityFrameworkCore;
using PostgresCrud.Data;
using PostgresCrud.Repositories;
using PostgresCrud.Services;

using PostgresCrud.Mappings; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure PostgreSQL database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Esta linha registra todos os Profiles de mapeamento do seu projeto
builder.Services.AddAutoMapper(typeof(MappingProfile));
// ----------------------------------

// Registra os Repositorios e os Serviços
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();