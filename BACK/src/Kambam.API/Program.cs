using Kambam.Domain.Interfaces;
using Kambam.Domain.Services;
using Kambam.Infra.Context;
using Kambam.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ICardRepository, CardRepository>();

// Configure Entity Framework Core for PostgreSQL with 'MyContext'
builder.Services.AddDbContext<MyContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

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
