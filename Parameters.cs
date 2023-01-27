
namespace ClientCredentials;

    public class Parameters
    {
        public string[] Args{ get; }
        public string Identity{ get; }
        public string Api{ get; }
        public string ClientId{ get; }
        public string ClientSecret{ get; }

        public Parameters(string[] args)
        {
            Args = args;
            Identity = Sensitive_data.Id;
            Api = Sensitive_data.API;
            ClientId = Sensitive_data.CID;
            ClientSecret = Sensitive_data.Cs;
        }

        private string GetParameter(string name)
        {
            var index = Array.IndexOf(Args, name);
            if( index == -1 ) return null;
            if( ++index > Args.Length -1 ) return null;
            return Args[index];
        }

        public bool IsValid => !string.IsNullOrWhiteSpace(Identity) &&
                               !string.IsNullOrWhiteSpace(Api) &&
                               !string.IsNullOrWhiteSpace(ClientId) &&
                               !string.IsNullOrWhiteSpace(ClientSecret);

        public string Usage => @$"
Invalid Parameters
Usage:
ClientCredentialsFixed.exe -Identity [{Sensitive_data.Id}] 
                           -Api [{Sensitive_data.API}]
                           -ClientId [{Sensitive_data.CID}]
                           -ClientSecret [{Sensitive_data.Cs}]
        ";
    }

