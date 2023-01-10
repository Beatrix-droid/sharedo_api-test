namespace ClientCredentials
{
    public class WorkId
    {
        public string resultTemplate {get;set;}
        public string? resultMessage {get;set;}
        public _info[] results {get;set;}

    }

    public class _info{
        public string entityId {get;set;}
        //data
        public Data data {get;set;}
        public View viewCommand{get;set;}
        public bool hasViewCommand {get;set;}
        public Command openCommand{get;set;}
        //opencommand
        public bool hasOpenCommand{get;set;}

    }

    public class Command{
        public string invokeType {get;set;}
        public string invoke {get;set;}
        public string config {get;set;}
    }

    public class View{
        public string invokeType {get;set;}
        public string invoke {get;set;}
        public string config {get;set;}

    }

    public class Data{
        public string reference {get;set;}
        public string title {get;set;}
        public string sharedoTypeIcon {get;set;}
        public string sharedoTypeName {get;set;}
        public string sharedoTypeColour {get;set;}
        public string phaseName {get;set;}
        public string phaseState {get;set;}
        public string? crumbs {get;set;}
    }
    }


