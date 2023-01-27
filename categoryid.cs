namespace ClientCredentials;

    public class CategoryId
    {
        public string id {get; set; } 
        public string sharedoTypeSystemName {get;set;}
        public string typePath {get;set;}

        public string phaseSystemName {get;set;}

        public int categoryId {get;set;}
        public string? title {get;set;}

        public bool titleIsUserProvided {get;set;}
        public string? userTitle{get;set;}
        public string? description{get;set;}

        public string reference {get;set;}
        public bool referenceIsUserProvided {get;set;}
        public string? externalReference{get;set;}
        public int priorityId {get;set;}
        public string? currencyCode {get;set;}
        public string? timeZone {get;set;}

    }

