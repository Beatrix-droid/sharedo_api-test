﻿


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
            string work_item_id = await ApiMethods.GetWorkID(config, token,"MDMWR/93478");

            //int category_id = await GetCategoryId(config, token, work_id);
            
            string task_id = await ApiMethods.CreateTask(config, token, work_item_id, $"title RPA - PO not approved. Chase.");
            await ApiMethods.UpdateTask(config, token, work_item_id, task_id);
            await ApiMethods.UpdateKeyFacts(config, token, work_item_id);
            
        }
    }




// assign task to matter owner

//update key facts (not in task)
//vm rpa process not yet approved 13.01