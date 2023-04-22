using Microsoft.EntityFrameworkCore;
using AnimeShop.Common;

namespace AnimeShop.Dal.DbContexts
{
	public sealed class NpgsqlContext : DbContext
	{
		public NpgsqlContext(DbContextOptions<NpgsqlContext> options)
			:base(options)
		{
			// Database.EnsureDeleted();
			Database.EnsureCreated();
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			RegistrateUser(modelBuilder);
			RegistrateProduct(modelBuilder);
			RegistrateAnimeShop(modelBuilder);
        }

		private void RegistrateUser(ModelBuilder modelBuilder)
		{
			var users = modelBuilder.Entity<User>();

			users.HasKey(u => u.Id);
			users.Property(u => u.FirstName).IsRequired();
			users.Property(u => u.SecondName).IsRequired();
			users.Property(u => u.Email).IsRequired();
			users.Property(u => u.Password).IsRequired();
		}

		private void RegistrateProduct(ModelBuilder modelBuilder)
		{
			var products = modelBuilder.Entity<Product>();

			products.HasKey(p => p.Id);
			products.Property(p => p.Name).IsRequired();
			products.Property(p => p.Amount).IsRequired();
			products.Property(p => p.Seasonal).IsRequired();
			products.Property(p => p.ProductType).IsRequired();
		}

		private void RegistrateAnimeShop(ModelBuilder modelBuilder)
		{
			var animeshops = modelBuilder.Entity<Common.AnimeShop>();

			animeshops.HasKey(a => a.Id);
			animeshops.Property(a => a.Name).IsRequired();
			animeshops.Property(a => a.MainUrl).IsRequired();
			animeshops.Property(a => a.AssortmentUpdateDate).IsRequired();
			animeshops.HasMany(a => a.Products);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Common.AnimeShop> AnimeShops { get; set; }
    }
}

