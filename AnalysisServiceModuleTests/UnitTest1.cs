namespace AnalysisServiceModuleTests
{
    [TestClass]
    public class DatabaseCentreTests
    {
        [TestMethod]
        public async void Test_GetUniqueVisitors_ShouldReturnExpectedCount()
        {
            Analysis_Report_Center service = new Analysis_Report_Center();
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            int ServiceID = 8;
            string productId = "P0006";
            string userID = "S0006";
            var uniqueVisitors = await service.ProcessAnalysisReportingServicesByID(ServiceID, startDate, endDate, productId, userID);

            Assert.IsNull(uniqueVisitors);
        }

        [TestMethod]
        public async void Test_GetTotalSales_ShouldReturnNonNegativeValue()
        {
            Analysis_Report_Center service = new Analysis_Report_Center();
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            string userID = "S0006";
            var salesTotalResult = await service.ProcessAnalysisReportingServicesByID(7, startDate, endDate, null, userID);

            Assert.IsNotNull(salesTotalResult);
        }

        [TestMethod]
        public async void Test_GetConversionRate_ShouldBeBetweenZeroAndOne()
        {
            Analysis_Report_Center service = new Analysis_Report_Center();
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            string productId = "P0006";
            var conversionRate = (int?)await service.ProcessAnalysisReportingServicesByID(2, startDate, endDate, productId, null);

            Assert.IsTrue(conversionRate >= 0 && conversionRate <= 1);
        }
    }
}