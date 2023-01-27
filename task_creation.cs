namespace ClientCredentials;
    public class AspectData
    {
        public string workScheduling { get; set; }
        public string taskDetails { get; set; }
        public string taskDueDate { get; set; }
        public string tags { get; set; }
        
        
        //these last two properties are relevant only in the case we are assigning an owner to an already existing task
        public string? task {get; set;}
        public string? taskAssignedTo {get; set;}
    }

    public class Create_A_Task
    {
        public string? instanceId { get; set; }
        public string parentSharedoId { get; set; }
        public string title { get; set; }
        public bool titleIsUserProvided { get; set; }
        public object categoryId { get; set; }
        public bool referenceIsUserProvided { get; set; }
        public string? description { get; set; }
        public string sharedoTypeSystemName { get; set; }
        public bool phaseIsOpen { get; set; }
        public int priorityId { get; set; }
        public AspectData aspectData { get; set; }
        public string originalSharedoType { get; set; }
        public List<object> relatedSharedos { get; set; }
    }


