using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientCredentials
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var config = new Parameters(args);
            if( !config.IsValid )
            {
                Console.Write(config.Usage);
                return;
            }

            var token = await GetToken();
            Console.WriteLine($"The token is {token}");
            (await GetProfile(config, token)).PrettyPrint();
        }

        static async Task<string> GetToken()
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"client.fixed:not a secret"));

            var body = new Dictionary<string, string>
            {
                { "grant_type", "Impersonate.Fixed" },
                { "scope", "sharedo"}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://vm-vnext-identity.sharedo.co.uk/connect/token");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Authorization", $"Basic {auth}");
            request.Content = new FormUrlEncodedContent(body);

            using(var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return responseBody.Access_Token;
            }
        }
        
        static async Task<UserInfoResponse> GetProfile(Parameters config, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{config.Api}/api/security/userInfo");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Authorization", $"Bearer {token}");

            using(var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                

                return await response.Content.ReadFromJsonAsync<UserInfoResponse>();
            }
        }
    }
}