using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.EntityFrameworkCore.Extensions;
using System.Text;
using WebApi.Entities;
using WebApi.Servicii;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servicii
builder.Services.AddScoped<ServiciuUtilizator>();
builder.Services.AddScoped<ServiciuInregistrare>();
builder.Services.AddScoped<ServiciuConectare>();
builder.Services.AddScoped<ServiciuAliment>();
builder.Services.AddScoped<ServiciuIstoric>();

// Jwt
var cheieJwt = builder.Configuration.GetSection("Jwt:Key").Get<string>();
var emitentJwt = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();

if (cheieJwt.IsNullOrEmpty() || emitentJwt.IsNullOrEmpty())
    throw new NullReferenceException("Cheie sau emitent nul in configuratie.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(optiuni =>
    {
        optiuni.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = emitentJwt,
            ValidAudience = emitentJwt,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cheieJwt))
        };
    });

// Azure MySql
builder.Services.AddEntityFrameworkMySQL()
    .AddDbContext<BdLicentaContext>(optiuni =>
    {
        optiuni.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

// Sesiune
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(optiuni =>
{
    optiuni.IdleTimeout = TimeSpan.FromMinutes(30);
    optiuni.Cookie.HttpOnly = true;
    optiuni.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.Run();
