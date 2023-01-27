using System.Net.Http.Json;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientCredentials;


public class ApiMethods{

// a class containing all the methods used in Main.cs
           public static async Task<string> GetToken(Parameters config)
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
        
        public static async Task<UserInfoResponse> GetProfile(Parameters config, string token)
         //a method that makes an api call to get information on a sharedo user. Takes the authentication token as a parameter
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



        public static async Task<string> GetWorkID(Parameters config, string token, string MatterReference)
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
        public static async Task<int> GetCategoryId(Parameters config, string token, string workItemID)
            // a method that returns a category id when we feed in its  work item's id: 
            {
                var timeSpan =  (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                var UnixEpoch = (long)timeSpan.TotalMilliseconds;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{config.Api}/api/sharedo/{workItemID}/base?_={UnixEpoch}");
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

        public static async Task<string> SharedoSysName(Parameters config, string token, string workItemID)
            // a method that returns a work id's system name when we feed in its  work item's id: 
            {
                var timeSpan =  (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                var UnixEpoch = (long)timeSpan.TotalMilliseconds;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{config.Api}/api/sharedo/{workItemID}/base?_={UnixEpoch}");
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

        public static async Task PostComment(Parameters config, string token, string workItemID, string yourComment)
            // a method that allows a user to post a comment to a specific workItemID
            {   
                HttpClient client = new HttpClient();
                HttpRequestMessage request;
                HttpResponseMessage response;

                string url = $"{config.Api}api/comments";

                request = new HttpRequestMessage(HttpMethod.Post, url);

                //create a sample comment object
                Comment new_comment = new Comment{
                        comment = $"<p>{yourComment}</p>",
                        sharedoId = workItemID,
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
                

            }
    public static async Task<PaymentRequestInfo> GetPayMentRequests(Parameters Config, string token, string workItemID)
            // a function that gets payment request info based on the work if you pass in
            {
                string url = $"{Config.Api}/api/listview/core-case-payments/20/1/paymentRequestDate/asc/?view=table&withCounts=1&contextId={workItemID}";
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
    public static async Task<string> CreateTask(Parameters Config, string token,  string workItemID,string task_title)
    {
        //a function that creates a task on sharedo for a particular matter. Accepts a task title and task description, and the work item id you want to
        // assign the task to. Returns the task id as a string
        HttpClient client = new HttpClient();
        HttpRequestMessage request;
        HttpResponseMessage response;

        string url = $"{Config.Api}/api/aspects/sharedos/";
        request = new HttpRequestMessage(HttpMethod.Post, url);
        DateTime dt =DateTime.Now;;
        String.Format("{0:s}", dt);

        //create a sample task object
        AspectData aspects = new AspectData{
            tags="{\"tags\":[]}",
            taskDetails="{}",
            taskDueDate="{\"dueDateTime\":\""    + String.Format("{0:s}", dt)+ "\""+     ",\"dueDateTime_timeZone\":\"Europe/London\",\"reminders\":[]}",
            workScheduling="{\"linkDueDateToExpectedStart\":false,\"linkDueDateToExpectedEnd\":true}",
        };
        

        
        List<Object> related = new List<object>{
        };

        Create_A_Task new_task = new Create_A_Task{
                                                    aspectData=aspects,
                                                    originalSharedoType="Task",
                                                    parentSharedoId=workItemID,
                                                    phaseIsOpen=true,
                                                    priorityId=9001,
                                                    referenceIsUserProvided=false,
                                                    relatedSharedos=related,
                                                    sharedoTypeSystemName="task",
                                                    title=task_title,
                                                    titleIsUserProvided=true
                                                };

        //serialise it and prepare the payload
        var JsonifiedTask= JsonConvert.SerializeObject(new_task);
        StringContent JsonString = new StringContent( JsonifiedTask, Encoding.UTF8, "application/json");
                
        //attach the payload to the request message
        request.Content= JsonString; //content we are sending across

        //now add the headers to authenticate:
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        //send request over and await the response
        response =await client.SendAsync(request);
        response.EnsureSuccessStatusCode(); //prints out an error if the response was not 200

        var responsebody= await response.Content.ReadAsStringAsync();
        string task_id=responsebody.ToString();
        task_id=task_id.Replace("\"", "");
        Console.WriteLine("task has been posted!");
        return task_id;        
        }


public static async Task UpdateTask(Parameters Config, string token,string workItemID, string task_id){
        //a function that assignes someone on sharedo for a particular task for a matter. 

        HttpClient client = new HttpClient();
        HttpRequestMessage request;
        HttpResponseMessage response;
        string url = $"{Config.Api}api/tasks/{task_id}/assign/ce8726c8-aae0-4820-80b3-a381edd889a3?includeResults";
        request = new HttpRequestMessage(HttpMethod.Post, url);
        
    
        EmptyJson emptyPayload= new EmptyJson{};

        //serialise it and prepare the payload
        var JsonifiedTask= JsonConvert.SerializeObject(emptyPayload);
        StringContent JsonString = new StringContent( JsonifiedTask, Encoding.UTF8, "application/json");
                
        //attach the payload to the request message
        request.Content= JsonString; //content we are sending across

        //now add the headers to authenticate:
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        //send request over and await the response
        response =await client.SendAsync(request);
        response.EnsureSuccessStatusCode(); //prints out an error if the response was not 200

        var responsebody= await response.Content.ReadAsStringAsync();
        Console.WriteLine("new task has been updated and assigned!");
    }

public static async Task UpdateKeyFacts(Parameters Config, string token, string workItemID)

    // a function that updates a particular matter case key facts:
    // the string after the formbuilder refers to the particular form we are updating (the case key facts form) so will remain constant.  The ctx_cateogyr id refers to a particular drop dwon we are selecting. This will remain constant too
    {
        HttpClient client = new HttpClient();
        HttpRequestMessage request;
        HttpResponseMessage response;
        
        string url =$"{Config.Api}/api/formbuilder/2381f919-9451-4242-96f0-52ecbb183f18?ctx_sharedoId={workItemID}&ctx_sharedoCategoryId=314171&ctx_jurisdiction=5002601&ctx_sharedoTypeSystemName=vm-matterdispute-defendant-pd-utilities";
        request = new HttpRequestMessage(HttpMethod.Post, url);

        KeyFactsAspectData keyaspects = new KeyFactsAspectData{
            formBuilder="{\"formData\":{\"vm-rpa-process-13-01\":\"500000128\"},\"sharedoId\":\"0f253147-d7c0-41a7-ac13-5037359be520\",\"formId\":\"2381f919-9451-4242-96f0-52ecbb183f18\",\"formIds\":[\"35D86F40-CBD1-4E55-8BCF-CB33C15C9EDD\",\"14b9fc48-9201-4157-99c7-8fb7036c3c33\",\"2381f919-9451-4242-96f0-52ecbb183f18\"]}",

            };
        UpdateKeyFacts keyFacts = new UpdateKeyFacts{
            id=workItemID,
            phaseIsOpen=true,
            priorityId=9001,
            phaseName="Litigation",
            referenceIsUserProvided=false,
            titleIsUserProvided=true,
            phaseSystemName="matter-dispute-defendant-litigation",
            sharedoTypeSystemName="matter-dispute-defendant-pi-public-liability",
            originalSharedoType="matter-dispute-defendant-pi-public-liability",
        };

        

                //serialise it and prepare the payload
                var JsonifiedComment= JsonConvert.SerializeObject(keyFacts);
                StringContent JsonString = new StringContent( JsonifiedComment, Encoding.UTF8, "application/json");

                //attach the payload to the request message
                request.Content= JsonString; //content we are sending across

                //now add the headers to authenticate:
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                //send request over and await the response
                response =await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Key facts updated successfully!");
    
    }
}