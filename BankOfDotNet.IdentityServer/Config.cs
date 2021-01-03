using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace BankOfDotNet.IdentityServer
{
    public class Config
    {
        public Config()
        {
        }

        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("bankOfDotNet", "Customer API for BankOfDotNet")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "bankOfDotNet" }
                }
            };
        }
    }
}
