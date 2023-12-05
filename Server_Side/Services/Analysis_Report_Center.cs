using Server_Side.DatabaseServices.Services.Models.Interfaces;
using Server_Side.Services.Analysis_Services;

public class Analysis_Report_Center
{
    //public List<UserView> Valid_User_Views_Table = new List<UserView>();
    //public List<PageView> Website_logs_table = new List<PageView>();
    //public List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
    //public List<Feedback> FeedbackTable = new List<Feedback>();

    public async Task<object?> ProcessAnalysisReportingServicesByID(int ServicesID, DateTime? startDate, DateTime? endDate, string? productId, string? userID)
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
                Dictionary<string, int>? processedBestCategory = await BestCategoryAnalysisservice.ProcessRequest();
                return processedBestCategory;
            case 2:
                ConversionRateService conversionRateService = new ConversionRateService(startDate.GetValueOrDefault(), endDate.GetValueOrDefault(), productId);
                //var conversionRateResult = await conversionRateService.ProcessRequest();
                return conversionRateService;
            case 3:
                FeedbackAnalysisService feedbackAnalysisService = new FeedbackAnalysisService(startDate, endDate);
                var processedFeedback = await feedbackAnalysisService.ProcessRequest();
                return processedFeedback;
            case 4:
                PageViewsService pageViewsService = new PageViewsService();
                var processedPageViews = await pageViewsService.ProcessRequest(startDate, endDate);
                return processedPageViews;
            case 5:
                PriceAnalysisService priceAnalysisService = new PriceAnalysisService();
                var processedPriceData = await priceAnalysisService.ProcessRequest(startDate, endDate);
                return processedPriceData;
            case 6:
                var processedTimeData = await TimeAnalysisService.ProcessRequest(startDate.Value, endDate.Value,productId);
                return processedTimeData;
            case 7:
                TotalSalesService totalSalesService = new TotalSalesService();
                var processedTotalSales = await totalSalesService.ProcessRequest(startDate, endDate, userID);
                int results = 0;
                return results as object;
            case 8:
                UniqueVisitorsService uniqueVisitorsService = new UniqueVisitorsService();
                var uniqueVisitorCount = await uniqueVisitorsService.GetUniqueVisitorCountAsync(startDate, endDate);
                return uniqueVisitorCount;
            default:
                throw new ArgumentException("Invalid table number");
        }
    }
}
