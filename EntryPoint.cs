namespace ClientCredentials;

    public class Program
    {
        public static async Task Main(string[] args)
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
            PaymentRequestInfo payments= await ApiMethods.GetPayMentRequests(config, token, work_id);
            string payment_id =payments.rows[0].id;
            
            //string task_id = await ApiMethods.CreateTask(config, token, work_id, $"title RPA - PO not approved. Chase.");
           // await ApiMethods.UpdateTask(config, token, work_id, task_id);

            await ApiMethods.UpdatePurchaseRequest(config, token, payment_id, "11112");
            
            
        }
    }