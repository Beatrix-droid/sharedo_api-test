using Newtonsoft.Json;

namespace ClientCredentials{

    public class RpaForm{
        public string sharedoId {get;set;}
        public Form formData {get;set;}

    }

    public class Form{
        
        [JsonProperty(PropertyName="vm-rpa-process-6-01")]
        public int vmRpaProcess601 {get;set;}
        
        [JsonProperty(PropertyName="vm-rpa-process-12")]
        public int vmRpaProcess12 {get; set;}

        [JsonProperty(PropertyName="vm-rpa-process-13-01")]
        public int vmRpaProcess1301 {get; set;}
    }
}

