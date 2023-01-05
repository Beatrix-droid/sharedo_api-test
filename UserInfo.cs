using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace Sharedo
{
    public class UserInfo{
        public static async Task<UserInfoResponse> GetProfile(string token){
            var request =  new HttpRequestMessage(HttpMethod.Get, $"{Program.IdentityBase}/ApplicationException/security/UserInfo");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Authorization", $"Bearer {token}");

            using (var client = new HttpClient()){
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<UserInfoResponse>();
            }

        }
    }

    public void GetProfile(string token){
            Console.WriteLine("hello");
    }
    public class UserInfoResponse{
        public bool IsAuthenticated {get; set;}
        public string ClientId {get; set;}
        public Guid? Userid {get; set;}
        public string Username {get; set;}
        public string Provider {get; set;}
        
        public string Firstname {get; set;}
        public string SurName {get; set;}
        public string FullName {get; set;}

        public Guid? OrganisationId {get; set;}
        public string Persona {get; set;}
        public List<string> GlobalPermissions {get; set;}

        public UserInfoResponse(){
            GlobalPermissions = new List<string>();
        }
        public void Prettyprint(){
            Console.WriteLine("{");
            Write("IsAuthenticated", IsAuthenticated);
            Write("ClientId", ClientId);
            Write("Userid", Userid);
            Write("UserName", Username);
            Write("Provider", Provider);
            Write("FirstName", Firstname);
            Write("Surname", SurName);
            Write("FullName", FullName);
            Write("OrganisationId", OrganisationId);
            Write("Persona", Persona);
            PrettyprintGlobalPermissions();
            Console.WriteLine("}");
        }
        public void PrettyprintGlobalPermissions(){
            const int max =2;
            var ouptut = String.Join(", ", GlobalPermissions.Take(max));
            if (GlobalPermissions.Count > max) ouptut+= $" [...+{GlobalPermissions.Count - max} more]";
            Write ("GlobalPermissions", output);
    }
    private void Write(string key, object output){
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"  {key}");
        Console.ResetColor();
        Console.WriteLine($": {output}");
    }
}