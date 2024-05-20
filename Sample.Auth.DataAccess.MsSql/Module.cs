using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Auth.DataAccess.MsSql.Contexts;

namespace Sample.Auth.DataAccess.MsSql
{
	public static class Module
	{
		private static string AssemblyName = typeof(Module).Assembly.GetName().Name;
		public static IServiceCollection AddAuthDataAccessMsSqlModule(
			this IServiceCollection services,
			string? connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException($"{nameof(connectionString)} is null or empty");
			}

			services.AddDbContext<ReadWriteDbContext>(options =>
				options.UseSqlServer(connectionString, action => action.MigrationsAssembly(AssemblyName)));

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<ReadWriteDbContext>();

			return services;
		}

		public static DbContextOptionsBuilder UseSqlServerWithCustomMigrationAssembly(
			this DbContextOptionsBuilder optionsBuilder,
			string? connectionString)
		{
			optionsBuilder.UseSqlServer(connectionString, options => options.MigrationsAssembly(AssemblyName));
			return optionsBuilder;
		}

	}
}