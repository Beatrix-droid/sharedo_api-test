
using System.Net.Http.Json;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            string work_id = await GetWorkID(config, token,"BISJQ"); 
            int category_id = await GetCategoryId(config, token, work_id);
            
            //string sys_name = await SharedoSysName(config, token, work_id);
            Console.WriteLine("the work id is " + work_id);
            
            //Console.WriteLine(await PostComment(config, token, work_id,"comment posted by Beatrice by calling the Api!"));
            //Console.WriteLine("the category id is " + category_id.ToString());
            //Console.WriteLine("the sharedo system name is " + sys_name);
            PaymentRequestInfo payment_request_info = await GetPayMentRequests(config, token, work_id);
            int payment_request_reference_number = Convert.ToInt32(payment_request_info.rows[0].data.reference);
            string payment_request_title = payment_request_info.rows[0].data.title;
.           string payment_request_subtitle = payment_request_info.rows[0].subTitle;
        
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
            // a method that retrieves a work Id item from a matter reference number. Takes the matter reference number and the authentication token as paramters
            {
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

        static async Task PostComment(Parameters config, string token, string workId, string yourComment)
            // a method that allows a user to post a comment to a specific workid
            {   
                HttpClient client = new HttpClient();
                HttpRequestMessage request;
                HttpResponseMessage response;

                string url = $"{config.Api}api/comments";

                request = new HttpRequestMessage(HttpMethod.Post, url);

                //create a sample comment object
                Comment new_comment = new Comment{
                        comment = $"<p>{yourComment}</p>",
                        sharedoId = workId,
                };

                //serialise it and prepare the payload
                var JsonifiedComment= JsonConvert.SerializeObject(new_comment);
                StringContent JsonString = new StringContent( JsonifiedComment, Encoding.UTF8, "application/json");

                //attach the payload to the request message
                request.Content= JsonString; //content we are sending across

                //now add the headers to authenticate:
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                //send request over and await the response
                response =await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responsebody =response.Content.ReadAsStringAsync();

                JObject obj = (JObject)JsonConvert.DeserializeObject(await responsebody);
                
                //Console.WriteLine($"Message has been sent!");
                //Console.WriteLine(responsebody);
            }
        

        static async Task<> GetPayMentRequests(Parameters Config, string token, string workid)
            // a function that gets payment request info based on the work if you pass in
            {
                string url = $"{Config.Api}/api/listview/core-case-payments/20/1/paymentRequestDate/asc/?view=table&withCounts=1&contextId={workid}";
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("Authorization", $"Bearer {token}");

                using(HttpClient client = new HttpClient())
                {
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var jsonString = await response.Content.ReadAsStreamAsync();
                    PaymentRequestInfo deserialised_json = await response.Content.ReadFromJsonAsync<PaymentRequestInfo>();
                
                    return deserialised_json;
                } 
            }


    }
}



