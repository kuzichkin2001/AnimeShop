using System;
using Microsoft.EntityFrameworkCore;
using AnimeShop.Common;

namespace AnimeShop.Dal.DbContexts
{
	public class NpgsqlContext : DbContext
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

		public DbSet<User> Users { get; set; }
    }
}

