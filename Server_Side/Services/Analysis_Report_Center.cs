using Server_Side.DatabaseServices.Services.Models.Interfaces;
using Server_Side.Services.Analysis_Services;

public class Analysis_Report_Center
{
    //public List<UserView> Valid_User_Views_Table = new List<UserView>();
    //public List<PageView> Website_logs_table = new List<PageView>();
    //public List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
    //public List<Feedback> FeedbackTable = new List<Feedback>();

    public async Task<object?> ProcessAnalysisReportingServicesByID(int ServicesID, DateTime? startDate, DateTime? endDate, string? productId)
    {
        List<Group_1_Record_Abstraction>? processedData = new List<Group_1_Record_Abstraction>();
        switch (ServicesID)
        {
            case 0:
                AverageOrderValueService averageOrderValueservice = new AverageOrderValueService(startDate, endDate);
                decimal? processedDataAverage = await averageOrderValueservice.ProcessRequest();
                return processedDataAverage;
            case 1:
                BestCategoryAnalysisService BestCategoryAnalysisservice = new BestCategoryAnalysisService(startDate, endDate);
                Dictionary<string, decimal>? processedBestCategory = await BestCategoryAnalysisservice.ProcessRequest();
                return processedBestCategory;
            //case 2:
            //    databaseServices = new SaleTransactionTableService();
            //    processedData = await databaseServices.GetDataServiceAsync();
            //    break;
            //case 3:
            //    databaseServices = new FeedbackTableService();
            //    processedData = await databaseServices.GetDataServiceAsync();
            //    break;
            //case 4:
            //    databaseServices = new SaleTransactionTableService();
            //    processedData = await databaseServices.GetDataServiceAsync();
            //    break;
            //case 5:
            //    databaseServices = new FeedbackTableService();
            //    processedData = await databaseServices.GetDataServiceAsync();
            //    break;
            //case 6:
            //    databaseServices = new SaleTransactionTableService();
            //    processedData = await databaseServices.GetDataServiceAsync();
            //    break;
            //case 7:
            //    databaseServices = new FeedbackTableService();
            //    processedData = await databaseServices.GetDataServiceAsync();
            //    break;
            //case 8:
            //    databaseServices = new SaleTransactionTableService();
            //    processedData = await databaseServices.GetDataServiceAsync();
            //    break;
            default:
                throw new ArgumentException("Invalid table number");
        }
    }
}
