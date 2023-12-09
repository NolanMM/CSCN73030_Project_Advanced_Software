using Server_Side.DatabaseServices.Services;
using Server_Side.DatabaseServices.Services.Interface_Service;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using Server_Side.DatabaseServices.Services.Network_Database_Services;
using System.Collections.Generic;

namespace Server_Side.DatabaseServices
{
    public class ProductItemData
    {
        public string ProductID { get; set; } = string.Empty;
        public string UserSeller { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string TodaySale { get; set; } = string.Empty;
        public string TodayViews { get; set; } = string.Empty;
        public string ProductPrices { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
    }
    public abstract class Database_Centre
    {
        private static string? Id { get; set; }
        private static readonly Random random = new Random();
        private static readonly HashSet<short> usedSessionIDs = new HashSet<short>();

        private static List<UserView>? Valid_User_Views_Table;
        private static List<PageView>? Website_logs_table;
        private static List<SaleTransaction>? SalesTransactionsTable;
        private static List<Feedback>? FeedbackTable;
        private static IDatabaseServices? databaseServices { get; set; }
        public static async Task<List<Group_1_Record_Abstraction>?> GetDataForDatabaseServiceID(int tablenumber)
        {
            Id = GenerateUniqueSessionID();
            List<Group_1_Record_Abstraction>? processedData = new List<Group_1_Record_Abstraction>();
            switch (tablenumber)
            {
                case 0:
                    databaseServices = new UserViewTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                case 1:
                    databaseServices = new PageViewTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                case 2:
                    databaseServices = new SaleTransactionTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                case 3:
                    databaseServices = new FeedbackTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                default:
                    return null;
            }
            return processedData;
        }
        private static string GenerateUniqueSessionID()
        {
            short sessionId;
            do
            {
                sessionId = (short)random.Next(0, 65536);
            } while (usedSessionIDs.Contains(sessionId));
            usedSessionIDs.Add(sessionId);
            return sessionId.ToString();
        }

        // Return list Dictionary<string,( string, string, string, string)              [Product name, Today sales total corresponding to that product id, total view corresponding to that product ID]

        public static async Task<List<ProductItemData>?> ProcessDataForGetTableCorrespondingUserID(string UserID, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<ProductItemData>? ReturnData = new List<ProductItemData>();
                // Retrieve data from the database
                Dictionary<(string, string), (string, string, string, string)>? return_Product_List_Database = await ProcessDataForGetTableCorrespondingUserID_Database(UserID);

                // Retrieve data from the product module
                Dictionary<string, (string, string, string, string)>? return_Product_List_Product_Module = await Product_Group_Database_Services.ProcessGetTableRequestByUserIDAsync(UserID);
                if (return_Product_List_Database != null && return_Product_List_Product_Module != null)
                {
                    // Create the ReturnData list
                    List<ProductItemData> filteredList = return_Product_List_Database
                        .Where(entry => return_Product_List_Product_Module.ContainsKey(entry.Key.Item1))
                        .Select(entry =>
                        {
                            var productId = entry.Key.Item1;
                            var dateKey = entry.Key.Item2;

                            var productModuleData = return_Product_List_Product_Module[productId];
                            var productModuleDatabase = entry.Value;

                            int.TryParse(productModuleDatabase.Item2, out int int_TodayView);

                            if (int_TodayView == 0)
                            {
                                int.TryParse(productModuleDatabase.Item3, out int IntTodaySale);
                                int_TodayView = IntTodaySale;
                            }

                            return new ProductItemData
                            {
                                ProductID = productId,
                                UserSeller = productModuleData.Item1,
                                ProductName = productModuleData.Item2,
                                TodaySale = productModuleDatabase.Item3,
                                TodayViews = int_TodayView.ToString(),
                                ProductPrices = productModuleData.Item3,
                                Date = productModuleDatabase.Item4
                            };
                        })
                        .ToList();

                    ReturnData = ProcessDataGetTable(filteredList, startDate, endDate);
                }
                return ReturnData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static List<ProductItemData>? ProcessDataGetTable(List<ProductItemData> productItemDatas, DateTime startDate, DateTime endDate)
        {
            if (productItemDatas != null)
            {
                // Store unique product IDs and their corresponding data
                Dictionary<string, ProductItemData> productDataDictionary = new Dictionary<string, ProductItemData>();

                foreach (var item in productItemDatas)
                {
                    DateTime itemDate = DateTime.Parse(item.Date);

                    // Check if the item is within the date range
                    if (itemDate >= startDate && itemDate <= endDate)
                    {
                        if (productDataDictionary.ContainsKey(item.ProductID))
                        {
                            var existingItem = productDataDictionary[item.ProductID];
                            existingItem.TodaySale = item.TodaySale;
                            existingItem.TodayViews = item.TodayViews;
                            existingItem.ProductPrices = item.ProductPrices;
                            existingItem.Date = item.Date;
                        }
                        else
                        {
                            productDataDictionary.Add(item.ProductID, item);
                        }
                    }
                }

                // Ensure that there is data record for each unique product ID for today; if not, set the value to 0
                foreach (var productId in productDataDictionary.Keys.ToList())
                {
                    var productData = productDataDictionary[productId];
                    DateTime productDate = DateTime.Parse(productData.Date);

                    if (productDate != startDate)
                    {
                        // Set values to 0 for TodaySale and TodayViews
                        productData.TodaySale = "0";
                        productData.TodayViews = "0";
                    }
                }

                // Convert the dictionary values back to a list and assign it to ReturnData
                productItemDatas = productDataDictionary.Values.ToList();
            }
            return productItemDatas;
        }

        public static async Task<Dictionary<(string, string), (string, string, string, string)>?> ProcessDataForGetTableCorrespondingUserID_Database(string UserID)
        {
            try
            {
                // Reset the data for each request
                Valid_User_Views_Table = new List<UserView>();
                Website_logs_table = new List<PageView>();
                SalesTransactionsTable = new List<SaleTransaction>();
                FeedbackTable = new List<Feedback>();

                // Define the Return Data
                Dictionary<string, (string, string, string)> return_List = new Dictionary<string, (string, string, string)>();

                // Collect the Userview data
                UserViewTableService userViewTableService = new UserViewTableService();
                List<Group_1_Record_Abstraction>? Userview_table_Data = await userViewTableService.GetDataServiceAsync();

                PageViewTableService pageViewTableService = new PageViewTableService();
                List<Group_1_Record_Abstraction>? pageView_table_Data = await pageViewTableService.GetDataServiceAsync();

                SaleTransactionTableService saleTransactionTableService = new SaleTransactionTableService();
                List<Group_1_Record_Abstraction>? saleTransaction_table_Data = await saleTransactionTableService.GetDataServiceAsync();

                if (Userview_table_Data != null && pageView_table_Data != null && saleTransaction_table_Data != null)
                {
                    Process_And_Print_Table_DataAsync(Userview_table_Data);
                    Process_And_Print_Table_DataAsync(pageView_table_Data);
                    Process_And_Print_Table_DataAsync(saleTransaction_table_Data);
                }

                Dictionary<(string, string), string>? return_Data_userView = userViewTableService.ProcessUserViewList(Valid_User_Views_Table, UserID);                          // (Product ID, Date), Count
                Dictionary<(string, string), string>? return_Data_PageView = pageViewTableService.ProcessPageViewList(Website_logs_table, UserID);                              // (Product ID, Date), Count
                Dictionary<(string, string), string>? return_Data_SaleTransactionTable = saleTransactionTableService.ProcessSaleTransactionList(SalesTransactionsTable, UserID);// (Product ID, Date),Total Quantity

                Dictionary<(string, string), (string, string, string, string)> combinedData = FullOuterJoin(return_Data_userView, return_Data_PageView, return_Data_SaleTransactionTable);

                return combinedData;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private static Dictionary<(string, string), (string, string, string, string)> FullOuterJoin(
        Dictionary<(string, string), string>? userViewData,
        Dictionary<(string, string), string>? pageViewData,
        Dictionary<(string, string), string>? saleTransactionData)
        {
            userViewData ??= new Dictionary<(string, string), string>();
            pageViewData ??= new Dictionary<(string, string), string>();
            saleTransactionData ??= new Dictionary<(string, string), string>();

            // Combine all unique ProductIDs and dates from the three dictionaries
            HashSet<(string, string)> allProductIDsAndDates = new HashSet<(string, string)>(userViewData.Keys);
            allProductIDsAndDates.UnionWith(pageViewData.Keys);
            allProductIDsAndDates.UnionWith(saleTransactionData.Keys);

            Dictionary<(string, string), (string, string, string, string)> result = new Dictionary<(string, string), (string, string, string, string)>();

            foreach ((string productId, string dateKey) in allProductIDsAndDates)
            {
                // Get data from each dictionary, defaulting to ("0", "") if not present
                string userViewCount = userViewData.TryGetValue((productId, dateKey), out var userViewValue)
                    ? userViewValue
                    : "0";

                string pageViewCount = pageViewData.TryGetValue((productId, dateKey), out var pageViewValue)
                    ? pageViewValue
                    : "0";

                string totalQuantity = saleTransactionData.TryGetValue((productId, dateKey), out var saleTransactionValue)
                    ? saleTransactionValue
                    : "0";

                string date_process = !string.IsNullOrEmpty(userViewCount) ? dateKey
                    : !string.IsNullOrEmpty(pageViewCount) ? dateKey
                    : !string.IsNullOrEmpty(totalQuantity) ? dateKey
                    : "0";

                // Add combined data to the result dictionary with a composite key
                result.Add((productId, date_process), (userViewCount, pageViewCount, totalQuantity, date_process));
            }

            return result;
        }



        private static bool Process_And_Print_Table_DataAsync(List<Group_1_Record_Abstraction> dataAsList)
        {
            try
            {
                Group_1_Record_Abstraction? firstElementOrDefault = dataAsList.FirstOrDefault();
                if (firstElementOrDefault is UserView userView_)
                {
                    Valid_User_Views_Table = new List<UserView>();
                }
                else if (firstElementOrDefault is PageView pageView_)
                {
                    Website_logs_table = new List<PageView>();
                }
                else if (firstElementOrDefault is SaleTransaction saleTransaction_)
                {
                    SalesTransactionsTable = new List<SaleTransaction>();
                }
                else if (firstElementOrDefault is Feedback feedback_)
                {
                    FeedbackTable = new List<Feedback>();
                }
                else
                {
                    Console.WriteLine("Unknown object type");
                }

                foreach (var Myobject in dataAsList)
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
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
