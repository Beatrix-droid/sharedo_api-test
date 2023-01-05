// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using System;

namespace Sharedo{

    public class TokenResponse{
        public string Acess_Token{get; set;}
        public int Expires_in{get; set;}
        public string Token_Type{get; set;}

    }
    public class Program{

        private  static string ClientId = Config_values.Id;
        private static string ClienSecret = Config_values.Secret;
        public static string IdentityBase = Config_values.Base;

        public static string ApiBase = Config_values.API_base;


        static async Task Main(){
            //application entry point 
            var token = await GetToken();
            Console.WriteLine($"Got a token: {token}");
        }
        static async Task<string> GetToken(){
            //all the stuff I am going to post to the api
            var body = new Dictionary<string, string>{
                { "grant_type", "client_credentials"},
                {"scope", "sharedo"},
                {"client_id", ClientId},
                {"client_secret", ClienSecret}
            };
            var request = new HttpRequestMessage(HttpMethod.Post, $"{IdentityBase}/connect/token");
            request.Headers.Add("accept", "application/json"); //so can deserialise easily
            request.Content = new FormUrlEncodedContent(body); //is the standard to have it form url encoded

            using (var client = new HttpClient()){
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();//throws an error if unsuccessful

                //read response body
                var responseBody = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return responseBody.Acess_Token;


            }


        }
    }
}

