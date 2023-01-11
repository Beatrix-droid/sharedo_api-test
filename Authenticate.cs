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
            Console.WriteLine(await GetWorkID(config, token,"D.TR.20.04861"));

            Console.WriteLine(await PostComment(config, token, "D.TR.20.04861", "comment posted with an api!"));

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
        static async Task<string> PostComment(Parameters config, string token, string MatterReference, string your_comment){
            
            //get the appropriate workid item and set up the api endpoint
            string work_id = await GetWorkID(config,token,MatterReference);
            var endpoint= new Uri($"{config.Api}/api/comments/");

            //create the comment
            Comment newcomment = new Comment(){
                comment=$"<p>{your_comment}</p>",
                sharedoId= $"{work_id}"
            };
            //convert it to json by serializing the class object
            var newcomment_json = JsonConvert.SerializeObject(newcomment);
            StringContent payload = new StringContent(newcomment_json, Encoding.UTF8,"application/json");
            //send it to the api

            

            using (HttpClient client = new HttpClient())
            {

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{config.Api}/api/comments/");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var result = await client.PostAsync(endpoint, payload);
            result.EnsureSuccessStatusCode();
            string message= "Comment posted successfully!";
            return message;
            
            }

        }
 }
}