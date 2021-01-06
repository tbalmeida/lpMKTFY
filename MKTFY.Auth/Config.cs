using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace MKTFY.Auth
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "mktfyapi",
                    DisplayName = "MKTFY API",
                    Scopes = { "mktfyapi.scope", "mktfyapi" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("mktfyapi.scope", "MKTFY API")
            };


        public static IEnumerable<Client> Clients(IConfiguration config)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mobile",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret(config.GetSection("Identity")
                                         .GetValue<string>("Secret")
                                         .Sha256())
                    },
                    AllowedScopes = { "mktfyapi.scope", IdentityServerConstants.StandardScopes.OpenId }
                }
            };
        }
    }
}
