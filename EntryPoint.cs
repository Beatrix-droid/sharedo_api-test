


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
            string work_id = await ApiMethods.GetWorkID(config, token,"BISJQ"); 
            //int category_id = await GetCategoryId(config, token, work_id);
            
            string task_id = await ApiMethods.CreateTask(config, token, work_id, $"post task no 105 with c#", "c# > uipath");
            await ApiMethods.UpdateTask(config, token, work_id, "new title!!", "updated description", task_id);

            Console.WriteLine(task_id);
            
        }
    }



// title RPA - PO not approved. Chase.
//no description required
// assign to matter owner
//due date is the current date
//update key facts (not in task)
//vm rpa process not yet approved 13.01