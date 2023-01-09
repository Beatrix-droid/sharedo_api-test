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

            var token = await GetToken(config);

            /// Commented below is a sample function call where a matter reference number is passed in, and the work item's entity id is returned
           // var work_id_response = await GetWorkID(config, token,"D.TR.20.04861");
            // Console.WriteLine(work_id_response.results[0].entityId);

            // call this snippet if you want to print out the token:
            // Console.WriteLine($"The token is {token}");
            //call this function if you want to get user info:
            // (await GetProfile(config, token)).PrettyPrint();
        }



        //a method that authenticates via the api and returns a token allowing one to impersonate a user
        static async Task<string> GetToken(Parameters config)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.ClientId}:{config.ClientSecret}"));

            var body = new Dictionary<string, string>
            {
                { "grant_type", "Impersonate.Fixed" },
                { "scope", "sharedo"}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{config.Identity}/connect/token");
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
        

        // a method that makes an api call to get information on a sharedo user. Takes the authentication token as a parameter
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


        // a method that retrieves a work Id item from a matter reference number. Takes the matter reference number and the authentication token as paramters

        static async Task<WorkId> GetWorkID(Parameters config, string token, string MatterReference){

                var timeSpan =  (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                var UnixEpoch = (long)timeSpan.TotalMilliseconds;

                var request = new HttpRequestMessage(HttpMethod.Get, $"{config.Api}/api/searches/quick/legal-cases/?&q={MatterReference}&_={UnixEpoch}");
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("Authorization", $"Bearer {token}");

                using(var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                

                return await response.Content.ReadFromJsonAsync<WorkId>();
            }
        }

        //string MatterReference = "WUBYA/66899";
        //string url= $"https://vm-vnext.sharedo.co.uk/api/searches/quick/legal-cases/?&q={MatterReference}&_=1673258376973";
    }
}