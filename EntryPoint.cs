


namespace ClientCredentials;

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
            var token = await ApiMethods.GetToken(config);
            //a sample call to get a work id
            WorkId workItemInfo= await ApiMethods.GetWorkID(config, token,"MXMCC/74748");
            
            string work_item_id=workItemInfo.results[0].entityId;
            string phase_name=workItemInfo.results[0].data.phaseName;
            string sharedo_type_name=workItemInfo.results[0].data.sharedoTypeName;
            //int category_id = await GetCategoryId(config, token, work_id);
            
            string task_id = await ApiMethods.CreateTask(config, token, work_item_id, $"title RPA - PO not approved. Chase.");
            await ApiMethods.UpdateTask(config, token, work_item_id, task_id);
            await ApiMethods.UpdateKeyFacts(config, token, work_item_id, phase_name, sharedo_type_name);
            
        }
    }




// assign task to matter owner
