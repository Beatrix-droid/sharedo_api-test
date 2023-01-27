using Newtonsoft.Json;

namespace ClientCredentials;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class KeyFactsAspectData
    {
        public string formBuilder { get; set; }
        public string? litigationDetails { get; set; }
        public string? incidentDetailsLocation { get; set; }
        public string? incidentDetailsDescription { get; set; }
        public string? legalProtocol { get; set; }
        public string? incidentDetailsType { get; set; }
        public string? matterJurisdiction { get; set; }
    }

    public class UpdateKeyFacts
    {
        public string? instanceId { get; set; }
        public string id { get; set; }
        public string? parentSharedoId { get; set; }
        public string? title { get; set; }
        public bool titleIsUserProvided { get; set; }
        public int? categoryId { get; set; }
        public string? reference { get; set; }
        public bool referenceIsUserProvided { get; set; }
        public object? externalReference { get; set; }
        public string? description { get; set; }
        public string sharedoTypeSystemName { get; set; }
        public string? phaseSystemName { get; set; }
        public string phaseName { get; set; }
        public bool phaseIsOpen { get; set; }
        public int priorityId { get; set; }
        public object? currencyCode { get; set; }
        public object? timeZone { get; set; }
        public KeyFactsAspectData aspectData { get; set; }
        public string? originalSharedoType { get; set;}

    }

