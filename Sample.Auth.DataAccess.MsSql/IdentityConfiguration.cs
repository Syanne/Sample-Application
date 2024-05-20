using IdentityServer4.Models;

namespace Sample.Auth.DataAccess.MsSql
{
	public class IdentityConfiguration
	{
		public static IEnumerable<IdentityResource> IdentityResources => new[]
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Profile(),
			new IdentityResource
			{
				Name = "role",
				UserClaims = new List<string> {"role"}
			}
		};

		public static IEnumerable<ApiScope> ApiScopes => new[]
		{
			new ApiScope("Sample.read"),
			new ApiScope("Sample.write")
		};

		public static IEnumerable<ApiResource> ApiResources => new[]
		{
			new ApiResource("SampleApi")
			{
				Scopes = new List<string> { "Sample.read" , "Sample.write" },
				ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
				UserClaims = new List<string> {"role"}
			}
		};

		public static IEnumerable<Client> Clients => new[]
		{
			new Client
			{
				ClientId = "sampleClient",
				ClientName = "Sample Console Client",
				AllowedGrantTypes = GrantTypes.ClientCredentials,
				ClientSecrets = {new Secret("ScopeSecret".Sha256())},
				AllowedScopes = { "Sample.read" , "Sample.write" }
			}
		};

	}
}
