using Server_Side.DatabaseServices.Services.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Analysis_Report_Center
{
    public List<UserView> Valid_User_Views_Table = new List<UserView>();
    public List<PageView> Website_logs_table = new List<PageView>();
    public List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
    public List<Feedback> FeedbackTable = new List<Feedback>();

    public Analysis_Report_Center(List<UserView> valid_User_Views_Table, List<PageView> website_logs_table, List<SaleTransaction> salesTransactionsTable, List<Feedback> feedbackTable)
    {
        Valid_User_Views_Table = valid_User_Views_Table;
        Website_logs_table = website_logs_table;
        SalesTransactionsTable = salesTransactionsTable;
        FeedbackTable = feedbackTable;
    }
    public async Task<bool> Process_AndPrint_Table_DataAsync(List<object> dataAsList)
    {
        try
        {
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
        catch (Exception ex)
        {
            string dataContent = "Error: " + ex.Message;
            return false;
        }
    }
}
