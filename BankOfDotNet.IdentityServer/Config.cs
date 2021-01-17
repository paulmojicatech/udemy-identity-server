using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace BankOfDotNet.IdentityServer
{
    public class Config
    {
        public Config()
        {
        }

        public static List<TestUser>GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Paul",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Kirstin",
                    Password = "password2"
                }
            };
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
                // Client Credential Based Client
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "bankOfDotNetApi" }
                },
                // Resource Password Based Client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "bankOfDotNetApi"}
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
