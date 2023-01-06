using System;

namespace ClientCredentials
{
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
            Identity = "https://vm-vnext-identity.sharedo.co.uk";
            Api = "https://vm-vnext.sharedo.co.uk/";
            ClientId = "client.fixed";
            ClientSecret = "not a secret";
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

        public string Usage => @"
Invalid Parameters
Usage:
ClientCredentialsFixed.exe -Identity [https://vm-vnext-identity.sharedo.co.uk]
                           -Api [https://vm-vnext.sharedo.co.uk/]
                           -ClientId [client.secret]
                           -ClientSecret [not a secret]
        ";
    }
}
