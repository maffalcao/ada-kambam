using System.Text;
using Kambam.API.Authentication;
using Kambam.Domain.Interfaces;
using Kambam.Infra.Context;
using Kambam.Infra.Repositories;
using Kambam.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Register AutoMapper using the assembly containing the Program class
builder.Services.AddAutoMapper(typeof(Program).Assembly);


// Configure JWT authentication for securing API endpoints.
builder.Services.AddAuthentication(jwt =>
{
    jwt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    jwt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

// Add services to the DI container.
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();

// Configure Entity Framework Core for PostgreSQL with 'MyContext'
builder.Services.AddDbContext<MyContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
