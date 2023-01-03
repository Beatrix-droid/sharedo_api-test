// See https://aka.ms/new-console-template for more information
namespace Sharedo{

    public class TokenResponse{
        public string Acess_token{get;set;}
        public int Expires_in{get; set;}
        public int Token_Type{get; set;}


    }
    public class Program{

        private static string CleintId ="sharedo-app";
        private static string ClienSecret = "not a secret";
        private static string IdentityBase = "https://localhost44351";
        static async Task Main(){
           //application entry point 
           var token = await GetToken();
           Console.WriteLine($"Got a token{token}");
        }
        static async Task<string> GetToken(){
            //all the stuff I am going to post to the api
            var body = new Dictionary<string, string>{
                { "grant_type", "client_credentials"},
                {"scope", "sharedo"},
                {"client_id", CleintId},
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
                return responseBody.Access_token;


            }


        }
    }
}

