using DowjonesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DowjonesAPI.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Entity> Entities { get; set; }
	}
}
