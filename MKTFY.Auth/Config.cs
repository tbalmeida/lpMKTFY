using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "mobile",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("UzKjRFnAHffxUFati8HMjSEzwMGgGHmN".Sha256())
                    },
                    AllowedScopes = { "mktfyapi.scope", IdentityServerConstants.StandardScopes.OpenId }
                }
            };
    }
}
