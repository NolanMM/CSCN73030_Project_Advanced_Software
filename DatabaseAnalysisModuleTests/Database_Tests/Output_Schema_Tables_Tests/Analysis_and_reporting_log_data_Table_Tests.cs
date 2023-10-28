using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;

namespace DatabaseAnalysisModuleTests.Database_Tests.Output_Schema_Tables_Tests
{
    [TestClass]
    public class Analysis_and_reporting_log_data_Table_Tests
    {
        // Test the Analysis_and_reporting_log_data_Table_Tests
        [TestMethod]
        public void TestTestConnectionToTable_log_data_table_test()
        {
            string session_ID = "log_data_table_test";
            var analysis_and_reporting_log_data_table = Analysis_and_reporting_log_data_table.SetUp(session_ID);
            bool isConnected = analysis_and_reporting_log_data_table.Test_Connection_To_Table();
            Assert.IsTrue(isConnected);
        }

        [TestMethod]
        public async Task TestCreateAsync_log_data_table_test()
        {
            string session_ID = "log_data_table_test";
            DateTime date = DateTime.Now;
            string request = "Test Request";
            string sessionID = "TestSessionID_Minh";

            var analysis_and_reporting_log_data_table = Analysis_and_reporting_log_data_table.SetUp(session_ID);
            bool created = await analysis_and_reporting_log_data_table.Create_Async(date, request, sessionID);
            Assert.IsTrue(created);
        }

        [TestMethod]
        public async Task TestReadAllAsync_log_data_table_test()
        {
            string session_ID = "log_data_table_test";
            var analysis_and_reporting_log_data_table = Analysis_and_reporting_log_data_table.SetUp(session_ID);
            var result = await analysis_and_reporting_log_data_table.Read_All_Async();
            Assert.IsNotNull(result);
        }
    }
}
