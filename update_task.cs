// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
namespace ClientCredentials;

public class UpdateAspectData{   
    
        public string workScheduling { get; set; }
        public string taskDetails { get; set; }
        public string taskDueDate { get; set; }
        public string tags { get; set; }
        
        public string task {get; set;}
        public string taskAssignedTo {get; set;}
    }


    public class Update_A_Task
    {
        public string instanceId { get; set; }
        public string id { get; set; }
        public string parentSharedoId { get; set; }
        public string title { get; set; }
        public bool titleIsUserProvided { get; set; }
        public object categoryId { get; set; }
        public string reference { get; set; }
        public bool referenceIsUserProvided { get; set; }
        public object externalReference { get; set; }
        public string description { get; set; }
        public string sharedoTypeSystemName { get; set; }
        public string phaseSystemName { get; set; }
        public string phaseName { get; set; }
        public bool phaseIsOpen { get; set; }
        public int priorityId { get; set; }
        public object currencyCode { get; set; }
        public object timeZone { get; set; }
        public UpdateAspectData aspectData { get; set; }
        public string originalSharedoType { get; set; }
    }

