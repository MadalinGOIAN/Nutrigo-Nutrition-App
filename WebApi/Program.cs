using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using WebApi.Entities;
using WebApi.Servicii;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ServiciuUtilizator>();
builder.Services.AddScoped<ServiciuInregistrare>();
builder.Services.AddScoped<ServiciuConectare>();
builder.Services.AddScoped<ServiciuAliment>();
builder.Services.AddScoped<ServiciuIstoric>();

builder.Services.AddEntityFrameworkMySQL()
    .AddDbContext<BdLicentaContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
