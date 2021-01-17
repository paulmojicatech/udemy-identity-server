using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static IdentityModel.OidcConstants;

namespace BankOfDotNet.ConsoleClient
{
    class MainClass
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var httpClient = new HttpClient();

            // Resource Owner Flow
            var discoveryRO = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5000");
            if (discoveryRO.IsError)
            {
                Console.WriteLine("\n" + discoveryRO.Error);
                return;
            }

            var tokensReqRO = new PasswordTokenRequest
            {
                Address = discoveryRO.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",
                GrantType = GrantTypes.Password,
                UserName = "Kirstin",
                Password = "password2",
                Scope = "bankOfDotNetApi"
            };

            var tokenRO = await httpClient.RequestPasswordTokenAsync(tokensReqRO);
            if (tokenRO.IsError)
            {
                Console.WriteLine("\n" + tokenRO.Error);
            }
            Console.WriteLine("\n" + tokenRO.Json + "\n");
            httpClient.SetBearerToken(tokenRO.AccessToken);
            // Client Credentials Flow
            //var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5000");
            //if (discovery.IsError)
            //{
            //    Console.WriteLine(discovery.Error);
            //    return;
            //}

            //var tokensReq = new ClientCredentialsTokenRequest
            //{
            //    Address = discovery.TokenEndpoint,
            //    ClientId = "client",
            //    ClientSecret = "secret",
            //    GrantType = GrantTypes.ClientCredentials,
            //    Scope = "bankOfDotNetApi"
            //};
            //var token = await httpClient.RequestClientCredentialsTokenAsync(tokensReq);
            //if (token.IsError)
            //{
            //    Console.WriteLine(token.Error);
            //}
            //Console.WriteLine("\n" + token.Json + "\n");

            //httpClient.SetBearerToken(tokenRO.AccessToken);
            var customerInfo = new StringContent(
                JsonConvert.SerializeObject(
                    new
                    {
                        Id = 10,
                        FirstName = "Manish",
                        LastName = "Ryan"
                    }
                ),
                Encoding.UTF8,
                "application/json"
            );
            var createCustomerRes = await httpClient.PostAsync("https://localhost:5001/api/customers", customerInfo);
            if (!createCustomerRes.IsSuccessStatusCode)
            {
                Console.WriteLine(createCustomerRes.StatusCode);
            }

            var getCustomerRes = await httpClient.GetAsync("https://localhost:5001/api/customers");
            if (!getCustomerRes.IsSuccessStatusCode)
            {
                Console.WriteLine(getCustomerRes.StatusCode);
            }
            else
            {
                var content = await getCustomerRes.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.Read();

        }
    }
}
