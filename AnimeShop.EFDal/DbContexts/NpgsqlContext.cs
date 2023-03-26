using Microsoft.EntityFrameworkCore;
using AnimeShop.Common;

namespace AnimeShop.Dal.DbContexts
{
	public sealed class NpgsqlContext : DbContext
	{
		public NpgsqlContext(DbContextOptions<NpgsqlContext> options)
			:base(options)
		{
			Database.EnsureCreated();
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			RegistrateUser(modelBuilder);
        }

		private void RegistrateUser(ModelBuilder modelBuilder)
		{
			var users = modelBuilder.Entity<User>();

			users.HasKey(u => u.Id);
			users.Property(u => u.Id).IsRequired();
			users.Property(u => u.Email).IsRequired();
			users.Property(u => u.Password).IsRequired();
		}

		private void RegistrateAnimeShop(ModelBuilder modelBuilder)
		{
			var animeshops = modelBuilder.Entity<Common.AnimeShop>();

			animeshops.HasKey(a => a.Id);
		}

		public DbSet<User> Users { get; set; }
    }
}

