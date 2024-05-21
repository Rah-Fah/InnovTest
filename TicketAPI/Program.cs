using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TicketAPI.Context;
using TicketAPI.Models;
using TicketAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("TicketManagerConnection");
if (string.IsNullOrEmpty(connectionString))
{
	// Gérer le cas où la chaîne de connexion est null ou vide
	throw new ArgumentNullException(nameof(connectionString), "ConnectionString cannot be null or empty.");
}
builder.Services.AddDbContext<TicketManagerDbContext>(x => x.UseNpgsql(connectionString));
builder.Services.AddScoped<IRepository<Tickets>, Repository<Tickets>>();
builder.Services.AddScoped<IRepository<Users>, Repository<Users>>();

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
