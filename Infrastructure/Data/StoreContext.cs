using System;
using System.Linq;
using System.Reflection;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
	public class StoreContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>,
    AppUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
	{
		public StoreContext(DbContextOptions<StoreContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
			{
				foreach (var entityType in modelBuilder.Model.GetEntityTypes())
				{
					var props = entityType.ClrType.GetProperties()
						.Where(p => p.PropertyType == typeof(decimal));

					var dateTimeProps = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset));

					foreach (var prop in props)
					{
						modelBuilder.Entity(entityType.Name).Property(prop.Name).HasConversion<double>();
					}

					foreach (var prop in dateTimeProps)
					{
						modelBuilder.Entity(entityType.Name).Property(prop.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
					}
				}
			}
		}
	}
}