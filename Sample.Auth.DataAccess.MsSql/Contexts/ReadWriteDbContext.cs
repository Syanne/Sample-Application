using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sample.Auth.DataAccess.MsSql.Entities;

namespace Sample.Auth.DataAccess.MsSql.Contexts
{
	public class ReadWriteDbContext : IdentityDbContext
	{
		public ReadWriteDbContext(DbContextOptions<ReadWriteDbContext> options) : base(options)
		{
		}

		public DbSet<Log> Logs { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<IdentityServer4.EntityFramework.Entities.Client>().HasData(new IdentityServer4.EntityFramework.Entities.Client
			{
				Id = 1,
				ClientId = "sampleClient",
				ClientName = "Sample Console Client"
			});

			builder.Entity<ClientSecret>().HasData(new ClientSecret
			{
				Id = 1,
				ClientId = 1,
				Value = "ScopeSecret".Sha256()
			});

			builder.Entity<IdentityServer4.EntityFramework.Entities.ApiScope>().HasData(
				new IdentityServer4.EntityFramework.Entities.ApiScope
				{
					Id = 1,
					Name = "Sample.read"
				},
				new IdentityServer4.EntityFramework.Entities.ApiScope
				{
					Id = 2,
					Name = "Sample.write"
				}
			);

			builder.Entity<ClientScope>().HasData(
				new ClientScope
				{
					Id = 1,
					ClientId = 1,
					Scope = "Sample.read"
				},
				new ClientScope
				{
					Id = 2,
					ClientId = 1,
					Scope = "Sample.write"
				}
			);

			builder.Entity<ClientGrantType>().HasData(
				new ClientGrantType
				{
					Id = 1,
					ClientId = 1,
					GrantType = GrantTypes.ClientCredentials.FirstOrDefault()
				});

			var openIdEntity = new IdentityResources.OpenId();
			var profileEntity = new IdentityResources.Profile();

			builder.Entity<IdentityServer4.EntityFramework.Entities.IdentityResource>().HasData(
				new IdentityServer4.EntityFramework.Entities.IdentityResource()
				{
					Id = 1,
					Name = "role"
				},
				new IdentityServer4.EntityFramework.Entities.IdentityResource()
				{
					Id = 2,
					Name = openIdEntity.Name,
					DisplayName = openIdEntity.DisplayName,
					Required = openIdEntity.Required,
					Emphasize = profileEntity.Emphasize
				},
				new IdentityServer4.EntityFramework.Entities.IdentityResource()
				{
					Id = 3,
					Name = profileEntity.Name,
					DisplayName = profileEntity.DisplayName,
					Emphasize = profileEntity.Emphasize,
					Required = profileEntity.Required
				}
			);

			var userId = Guid.NewGuid().ToString();
			builder.Entity<IdentityUser>().HasData(
				new IdentityUser() 
				{
					Id = userId,
					UserName = "sampleUser",
					Email = "sampleUser@test.com",
					EmailConfirmed = true,
					PasswordHash = "AQAAAAIAAYagAAAAEInPOSEzfrPuyZ1UUFtYBd8d5nkWBjujiUAdcrkuaAQ4C4LitK5AmyHzOlui5L1QtQ=="
				});
		}
	}
}
