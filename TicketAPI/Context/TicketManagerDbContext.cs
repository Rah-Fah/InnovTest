using Microsoft.EntityFrameworkCore;
using TicketAPI.Models;

namespace TicketAPI.Context
{
	public class TicketManagerDbContext : DbContext
	{
		public DbSet<Users> User { get; set; }
		public DbSet<Tickets> Ticket { get; set; }

		public TicketManagerDbContext(DbContextOptions<TicketManagerDbContext> options) : base(options)
		{
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("TicketManagerConnection");
			optionsBuilder.UseNpgsql(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configurer d'autres aspects du modèle de données
			modelBuilder.Entity<Tickets>().HasKey(t => t.IdTicket);
			modelBuilder.Entity<Users>().HasKey(t => t.IdUser);
		}
	}
}
