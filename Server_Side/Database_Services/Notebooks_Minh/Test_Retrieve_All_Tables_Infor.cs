//==================================================================================================================================
/*                                                              **Minh_Note_1**
                                Test Retrieve and Print All data from the table that their class have been created

//using System;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using Server_Side.Database_Services;
//using Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class;
//using static Server_Side.Services.Analysis_Report_Services;
//using System.Runtime.CompilerServices;

//namespace Server_Side
//{
//    class Program
//    {
//        static async Task Main(string[] args)
//        {
//            // Initialize the Database_Analysis_And_Reporting_Services_Control instance
//            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();

//            // Take the SessionID input 
//            Console.WriteLine("Enter the SessionID: ");
//            string SessionID = Console.ReadLine();

//            // Initialize and retrieve tables
//            await databaseControl.Initialize_Tables(SessionID);

//            // Retrieve all tables' data
//            await databaseControl.Retrieve_All_Tables();
//            Task.WaitAll();

//            await databaseControl.Database_Services_Control_NotificationAsync("Retrieve All Data", SessionID);
//            //Console.ReadLine();
//            //Console.WriteLine("============================================");

//            // Print the data from the Database_Analysis_And_Reporting_Services_Control
//            //Console.WriteLine("UserView Data:");
//            Database_Analysis_And_Reporting_Services_Control.Print_UserData(databaseControl.Valid_User_Views_Table);
//            //Console.WriteLine("PageView Data:");
//            Database_Analysis_And_Reporting_Services_Control.Print_PageViewData(databaseControl.Website_logs_table);
//            //Console.WriteLine("SaleTransaction Data:");
//            Database_Analysis_And_Reporting_Services_Control.Print_SaleTransactionData(databaseControl.SalesTransactionsTable);
//            //Console.WriteLine("Feedback Data:");
//            Database_Analysis_And_Reporting_Services_Control.Print_FeedbackData(databaseControl.FeedbackTable);
//            Console.ReadLine();
//        }
//    }
//}
*/
//==================================================================================================================================
/*                                                              **Minh_Note_2**
                                    Modify this to AnalysisServices.cs to integrate with the Database_Services


                using Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class;
                using Server_Side.Database_Services;
                private readonly List<UserView> Valid_User_Views_Table = new List<UserView>();
                private readonly List<PageView> Website_logs_table = new List<PageView>();
                private readonly List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
                private readonly List<Feedback> FeedbackTable = new List<Feedback>();
                private string Services_Types { get; set; } = "Retrieve Data";

                public class UserView
                {
                    public string User_Id { get; set; }
                    public DateTime Timestamp { get; set; }
                    public DateOnly End_Date { get; set; }
                    public DateOnly Start_Date { get; set; }
                }

                public class PageView
                {
                    public string SessionId { get; set; }
                    public string UserId { get; set; }
                    public string PageUrl { get; set; }
                    public string PageInfo { get; set; }
                    public string ProductId { get; set; }
                    public DateTime DateTime { get; set; }
                    public DateTime Start_Time { get; set; }
                    public DateTime End_Time { get; set; }
                }

                public class SaleTransaction
                {
                    public string TransactionId { get; set; }
                    public string UserId { get; set; }
                    public decimal TransactionValue { get; set; }
                    public DateTime Date { get; set; }
                }

                public class Feedback
                {
                    public int FeedbackId { get; set; }
                    public string UserId { get; set; }
                    public string ProductId { get; set; }
                    public decimal StarRating { get; set; }
                }

                public void InitializeData(string SessionID)
                {
                    Services_Types = "Retrieve All Data";
                    Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
                    databaseControl.InitializeTables(SessionID).Wait();
                    databaseControl.RetrieveAllTables().Wait();
                    databaseControl.Database_Services_Control_NotificationAsync(Services_Types, SessionID).Wait();
                    Valid_User_Views_Table.AddRange(databaseControl.Valid_User_Views_Table);
                    Website_logs_table.AddRange(databaseControl.Website_logs_table);
                    SalesTransactionsTable.AddRange(databaseControl.SalesTransactionsTable);
                    FeedbackTable.AddRange(databaseControl.FeedbackTable);
                }
*/