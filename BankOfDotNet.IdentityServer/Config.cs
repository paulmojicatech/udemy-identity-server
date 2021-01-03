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
                new ApiResource("bankOfDotNetApi", "Customer API for BankOfDotNet")
                {
                    Scopes = { "bankOfDotNetApi" }
                }
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
                    AllowedScopes = { "bankOfDotNetApi" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("bankOfDotNetApi", "Read Data"),
                new ApiScope("write", "Write Data"),
                new ApiScope("delete", "Delete Data")
            };
        }
    }
}
