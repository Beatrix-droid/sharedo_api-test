using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClientCredentials
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Parameters config = new Parameters(args);
            if( !config.IsValid )
            {
                Console.Write(config.Usage);
                return;
            }

            var token = await GetToken(config);

          //a sample call to get a work id

        string work_id = await GetWorkID(config, token,"D.TR.20.04861");
        int category_id = await GetCategoryId(config, token, work_id);
        string sys_name = await SharedoSysName(config, token, work_id);

        Console.WriteLine("the category id is " + category_id.ToString());
        Console.WriteLine("the sharedo system name is " + sys_name);
        }



        static async Task<string> GetToken(Parameters config)
        //a method that authenticates via the api and returns a token allowing one to impersonate a user
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.ClientId}:{config.ClientSecret}"));

            var body = new Dictionary<string, string>
            {
                { "grant_type", "Impersonate.Fixed" },
                { "scope", "sharedo"}
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{config.Identity}/connect/token");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Authorization", $"Basic {auth}");
            request.Content = new FormUrlEncodedContent(body);

            using(HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                TokenResponse responseBody = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return responseBody.Access_Token;
            }
        }
        

        static async Task<UserInfoResponse> GetProfile(Parameters config, string token)
        // a method that makes an api call to get information on a sharedo user. Takes the authentication token as a parameter
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{config.Api}/api/security/userInfo");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Authorization", $"Bearer {token}");

            using(HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                

                return await response.Content.ReadFromJsonAsync<UserInfoResponse>();
            }
        }



        static async Task<string> GetWorkID(Parameters config, string token, string MatterReference)
        {
         // a method that retrieves a work Id item from a matter reference number. Takes the matter reference number and the authentication token as paramters
                var timeSpan =  (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                var UnixEpoch = (long)timeSpan.TotalMilliseconds;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{config.Api}/api/searches/quick/legal-cases/?&q={MatterReference}&_={UnixEpoch}");
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("Authorization", $"Bearer {token}");

                using(HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                

                WorkId deserialised_json = await response.Content.ReadFromJsonAsync<WorkId>();
                string id = deserialised_json.results[0].entityId;
                return id;
            }
        }


        static async Task<int> GetCategoryId(Parameters config, string token, string workid)
        // a method that returns a category id when we feed in its  work item's id: 

        {
                var timeSpan =  (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                var UnixEpoch = (long)timeSpan.TotalMilliseconds;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{config.Api}/api/sharedo/{workid}/base?_={UnixEpoch}");
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("Authorization", $"Bearer {token}");

                using(HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                CategoryId deserialised_json = await response.Content.ReadFromJsonAsync<CategoryId>();
                int categoryId = deserialised_json.categoryId;
                return categoryId;
            }

        }

        static async Task<string> SharedoSysName(Parameters config, string token, string workid)
        // a method that returns a work id's system name when we feed in its  work item's id: 

        {
                var timeSpan =  (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                var UnixEpoch = (long)timeSpan.TotalMilliseconds;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{config.Api}/api/sharedo/{workid}/base?_={UnixEpoch}");
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("Authorization", $"Bearer {token}");

                using(HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                CategoryId deserialised_json = await response.Content.ReadFromJsonAsync<CategoryId>();
                string sysName = deserialised_json.sharedoTypeSystemName;
                return sysName;


    
        }
    }
    }
}
