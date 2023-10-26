using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class;
using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;
using System;
using System.Threading.Tasks;
namespace DatabaseAnalysisModuleTests
{
    [TestClass]
    public class FeedbackTableTests
    {
        // Test the Feedback table
        [TestMethod]
        public void Initialize_Feedback_table()
        {
            string session_ID = "feedback_table_1";
            var feedbackTable = Feedback_table.SetUp(session_ID);
            Assert.IsNotNull(feedbackTable);
        }

        [TestMethod]
        public void TestTestConnectionToTable_Feedback_table()
        {
            string session_ID = "feedback_table_2";
            var feedbackTable = Feedback_table.SetUp(session_ID);
            bool isConnected = feedbackTable.Test_Connection_To_Table();
            Assert.IsTrue(isConnected);
        }

        [TestMethod]
        public async Task TestReadAllAsync_Feedback_table()
        {
            string session_ID = "feedback_table_3";
            var feedbackTable = Feedback_table.SetUp(session_ID);
            var result = await feedbackTable.ReadAllAsync();
            Assert.IsNotNull(result);
        }

        // Test the Saletransaction table
        [TestMethod]
        public void Initialize_Salestransaction_table()
        {
            string session_ID = "salestransaction_table_1";
            var salestransaction_table = Salestransaction_table.SetUp(session_ID);
            Assert.IsNotNull(salestransaction_table);
        }

        [TestMethod]
        public void TestTestConnectionToTable_Salestransaction_table()
        {
            string session_ID = "salestransaction_table_2";
            var salestransaction_table = Salestransaction_table.SetUp(session_ID);
            bool isConnected = salestransaction_table.Test_Connection_To_Table();
            Assert.IsTrue(isConnected);
        }

        [TestMethod]
        public async Task TestReadAllAsync_Salestransaction_table()
        {
            string session_ID = "salestransaction_table_3";
            var salestransaction_table = Salestransaction_table.SetUp(session_ID);
            var result = await salestransaction_table.ReadAllAsync();
            Assert.IsNotNull(result);
        }

        // Test the Pageview_table
        [TestMethod]
        public void Initialize_Pageview_table()
        {
            string session_ID = "pageview_table_1";
            var pageview_table = Pageview_table.SetUp(session_ID);
            Assert.IsNotNull(pageview_table);
        }

        [TestMethod]
        public void TestTestConnectionToTable_Pageview_table()
        {
            string session_ID = "pageview_table_2";
            var pageview_table = Pageview_table.SetUp(session_ID);
            bool isConnected = pageview_table.Test_Connection_To_Table();
            Assert.IsTrue(isConnected);
        }

        [TestMethod]
        public async Task TestReadAllAsync_Pageview_table()
        {
            string session_ID = "pageview_table_3";
            var pageview_table = Pageview_table.SetUp(session_ID);
            var result = await pageview_table.ReadAllAsync();
            Assert.IsNotNull(result);
        }

        // Test the Userview_table
        [TestMethod]
        public void Initialize_Userview_table()
        {
            string session_ID = "userview_table_1";
            var userview_table = Userview_table.SetUp(session_ID);
            Assert.IsNotNull(userview_table);
        }

        [TestMethod]
        public void TestTestConnectionToTable_Userview_table()
        {
            string session_ID = "userview_table_3";
            var userview_table = Userview_table.SetUp(session_ID);
            bool isConnected = userview_table.Test_Connection_To_Table();
            Assert.IsTrue(isConnected);
        }

        [TestMethod]
        public async Task TestReadAllAsync_Userview_table()
        {
            string session_ID = "userview_table_2";
            var userview_table = Userview_table.SetUp(session_ID);
            var result = await userview_table.ReadAllAsync();
            Assert.IsNotNull(result);
        }

        // Test the Analysis_and_reporting_log_data_table
        [TestMethod]
        public void TestTestConnectionToTable_log_data_table_test()
        {
            string session_ID = "log_data_table_test";
            var analysis_and_reporting_log_data_table = Analysis_and_reporting_log_data_table.SetUp(session_ID);
            bool isConnected = analysis_and_reporting_log_data_table.Test_Connection_To_Table();
            Assert.IsTrue(isConnected);
        }

        [TestMethod]
        public async Task TestCreateAsync()
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
        public async Task TestReadAllAsync()
        {
            string session_ID = "log_data_table_test";
            var analysis_and_reporting_log_data_table = Analysis_and_reporting_log_data_table.SetUp(session_ID);
            var result = await analysis_and_reporting_log_data_table.Read_All_Async();
            Assert.IsNotNull(result);
        }
    }
}
