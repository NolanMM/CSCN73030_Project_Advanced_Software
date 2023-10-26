namespace Server_Side.Database_Services
{
    /* This class for data models and database-related services */
    using Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class;
    using Server_Side.Database_Services.Table_Interface;
    using MySqlConnector;
    using static Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class.Userview_table;
    using static Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class.Pageview_table;
    using static Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class.Salestransaction_table;
    using static Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class.Feedback_table;
    class Database_Analysis_And_Reporting_Services_Control
    {
        public List<UserView> Valid_User_Views_Table = new List<UserView>();
        public List<PageView> Website_logs_table = new List<PageView>();
        public List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
        public List<Feedback> FeedbackTable = new List<Feedback>();

        private readonly string Input_schemma = "analysis_and_reporting_raw_data";
        private readonly string Log_schemma = "analysis_and_reporting_log_data";
        private List<(string, string)> table_names_link_session_id_list;
        // List hold the table name and the table object link with the session ID
        private List<(string, string, Input_Tables_Template)> table_names_link_session_id_table_oject_list;
        public List<(string, List<List<object>>)> All_Tables_Data_Retrive_Link_With_Session_ID; // turn to private later
        private List<string> table_names_list;

        private static Database_Analysis_And_Reporting_Services_Control instance;
        private Database_Analysis_And_Reporting_Services_Control()
        {
            table_names_link_session_id_list = new List<(string, string)>();
            table_names_link_session_id_table_oject_list = new List<(string, string, Input_Tables_Template)>();
            table_names_list = new List<string>();
        }
        public static Database_Analysis_And_Reporting_Services_Control Retrieve_Database_Analysis_And_Reporting_Services_Control()
        {
            if (instance == null)
            {
                instance = new Database_Analysis_And_Reporting_Services_Control();
            }
            return instance;
        }
        public void ProcessAndPrintTableData(string tableName, List<List<object>> dataAsList)
        {
            //Console.WriteLine($"{tableName} Data:");

            foreach (var data in dataAsList)
            {
                foreach (var Myobject in data)
                {
                    if (Myobject is UserView userView)
                    {
                        Valid_User_Views_Table.Add(userView);
                        //Console.WriteLine($"User_Id: {userView.User_Id}, Timestamp: {userView.Timestamp}, End_Date: {userView.End_Date}, Start_Date: {userView.Start_Date}");
                    }
                    else if (Myobject is PageView pageView)
                    {
                        Website_logs_table.Add(pageView);
                        //Console.WriteLine($"SessionId: {pageView.SessionId}, UserId: {pageView.UserId}, PageUrl: {pageView.PageUrl}, PageInfo: {pageView.PageInfo}, ProductId: {pageView.ProductId}, DateTime: {pageView.DateTime}, Start_Time: {pageView.Start_Time}, End_Time: {pageView.End_Time}");
                    }
                    else if (Myobject is SaleTransaction saleTransaction)
                    {
                        SalesTransactionsTable.Add(saleTransaction);
                        //Console.WriteLine($"TransactionId: {saleTransaction.TransactionId}, UserId: {saleTransaction.UserId}, TransactionValue: {saleTransaction.TransactionValue}, Date: {saleTransaction.Date}");
                    }
                    else if (Myobject is Feedback feedback)
                    {
                        FeedbackTable.Add(feedback);
                        //Console.WriteLine($"FeedbackId: {feedback.FeedbackId}, UserId: {feedback.UserId}, ProductId: {feedback.ProductId}, StarRating: {feedback.StarRating}");
                    }
                    else
                    {
                        Console.WriteLine("Unknown object type");
                    }
                }
            }
        }
        public async Task RetrieveAllTables()
        {
            All_Tables_Data_Retrive_Link_With_Session_ID = new List<(string, List<List<object>>)>();

            foreach (var (tableName, sessionID, table) in table_names_link_session_id_table_oject_list)
            {
                List<object>? tableData = await table.ReadAllAsync();
                if (tableData != null)
                {
                    List<object> dataAsList = tableData.ToList();
                    All_Tables_Data_Retrive_Link_With_Session_ID.Add((tableName, new List<List<object>> { dataAsList }));
                }
            }
            foreach (var (tableName, dataAsList) in All_Tables_Data_Retrive_Link_With_Session_ID)
            {
                ProcessAndPrintTableData(tableName, dataAsList);
            }
        }

        public async Task InitializeTables(string sessionID)
        {
            await RetrieveTableNameList();

            foreach (var tableName in table_names_list)
            {
                Input_Tables_Template table = CreateTableForName(tableName, sessionID);

                if (table != null)
                {
                    // Store the table in the list along with the table name and session ID.
                    table_names_link_session_id_table_oject_list.Add((tableName, sessionID, table));
                }
            }
        }
        private Input_Tables_Template? CreateTableForName(string tableName, string sessionID)
        {
            switch (tableName)
            {
                case "feedback_table":
                    return Feedback_table.SetUp(sessionID);
                case "pageview":
                    return Pageview_table.SetUp(sessionID);
                case "sales_transaction_table":
                    return Salestransaction_table.SetUp(sessionID);
                case "userview":
                    return Userview_table.SetUp(sessionID);
                default:
                    Console.WriteLine(tableName + " template does not exist");
                    return null;
            }
        }

        public async Task<Input_Tables_Template?> Retrieve_Connection_To_TableAsync(string table_name, string session_ID)
        {
            await RetrieveTableNameList();
            var existingTable = table_names_link_session_id_table_oject_list
            .FirstOrDefault(t => t.Item1 == table_name && t.Item2 == session_ID);
            if (existingTable != default)
            {
                return existingTable.Item3;
            }
            else
            {
                if (table_names_list.Contains(table_name))
                {
                    Input_Tables_Template temp;
                    switch (table_name)
                    {
                        case "feedback_table":
                            temp = Feedback_table.SetUp(session_ID);
                            return temp;
                            break;
                        case "pageview":
                            temp = Pageview_table.SetUp(session_ID);
                            return temp;
                            break;

                        case "sales_transaction_table":
                            temp = Salestransaction_table.SetUp(session_ID);
                            return temp;
                            break;

                        case "userview":
                            temp = Userview_table.SetUp(session_ID);
                            return temp;
                            break;
                        default:
                            Console.WriteLine("Table name does not exist");
                            return null;
                    }
                }
                else
                {
                    Console.WriteLine("Table name does not exist");
                    return null;
                }
            }
        }

        public async Task<List<string>>? RetrieveTableNameList()
        {
            string constring = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + Input_schemma + ";SslMode=Required";
            try
            {
                MySqlConnection Connection = new MySqlConnection(constring);
                Connection.Open();
                if (Connection != null)
                {
                    string sql = "SELECT table_name FROM information_schema.tables WHERE table_schema = '" + Input_schemma + "'";
                    var cmd = new MySqlCommand(sql, Connection);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var Table_Namme = reader.GetString(0);
                        table_names_list.Add(Table_Namme);
                        //Console.WriteLine($"Table_Namme: {Table_Namme}");
                    }
                }
                return table_names_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public void PrintUserData(List<UserView> userViews)
        {
            Console.WriteLine("UserView Data:");
            foreach (var userView in userViews)
            {
                Console.WriteLine($"User_Id: {userView.User_Id}, Timestamp: {userView.Timestamp}, End_Date: {userView.End_Date}, Start_Date: {userView.Start_Date}");
            }
        }

        public void PrintPageViewData(List<PageView> pageViews)
        {
            Console.WriteLine("PageView Data:");
            foreach (var pageView in pageViews)
            {
                Console.WriteLine($"SessionId: {pageView.SessionId}, UserId: {pageView.UserId}, PageUrl: {pageView.PageUrl}, PageInfo: {pageView.PageInfo}, ProductId: {pageView.ProductId}, DateTime: {pageView.DateTime}, Start_Time: {pageView.Start_Time}, End_Time: {pageView.End_Time}");
            }
        }

        public void PrintSaleTransactionData(List<SaleTransaction> saleTransactions)
        {
            Console.WriteLine("SaleTransaction Data:");
            foreach (var saleTransaction in saleTransactions)
            {
                Console.WriteLine($"TransactionId: {saleTransaction.TransactionId}, UserId: {saleTransaction.UserId}, TransactionValue: {saleTransaction.TransactionValue}, Date: {saleTransaction.Date}");
            }
        }

        public void PrintFeedbackData(List<Feedback> feedbackData)
        {
            Console.WriteLine("Feedback Data:");
            foreach (var feedback in feedbackData)
            {
                Console.WriteLine($"FeedbackId: {feedback.FeedbackId}, UserId: {feedback.UserId}, ProductId: {feedback.ProductId}, StarRating: {feedback.StarRating}");
            }
        }
    }
}
