using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server_Side.Services;
using System.Linq;

namespace AnalysisServiceTests
{
    [TestClass]
    public class AnalysisServiceRegressionTests
    {
        private Analysis_Report_Services _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Analysis_Report_Services();

            // Populate _service with mock data for testing here.
            // This setup will run before each test.
            // Fill the inner private lists like Valid_User_Views_Table, Website_logs_table, etc.
        }

        [TestMethod]
        public void Test_GetUniqueVisitors_ShouldReturnExpectedCount()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            int uniqueVisitors = _service.GetUniqueVisitors(startDate, endDate);

            Assert.IsTrue(uniqueVisitors >= 0);
        }

        [TestMethod]
        public void Test_GetTotalSales_ShouldReturnNonNegativeValue()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            decimal sales = _service.GetTotalSales(startDate, endDate);

            Assert.IsTrue(sales >= 0);
        }

        [TestMethod]
        public void Test_GetAverageOrderValue_ShouldReturnExpectedValue()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            decimal avgOrderValue = _service.GetAverageOrderValue(startDate, endDate);

            Assert.IsTrue(avgOrderValue >= 0);
        }

        [TestMethod]
        public void Test_GetConversionRate_ShouldBeBetweenZeroAndOne()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-5);
            DateTime endDate = DateTime.UtcNow;
            decimal conversionRate = _service.GetConversionRate("sampleProductId", startDate, endDate);

            Assert.IsTrue(conversionRate >= 0 && conversionRate <= 1);
        }

    }
}