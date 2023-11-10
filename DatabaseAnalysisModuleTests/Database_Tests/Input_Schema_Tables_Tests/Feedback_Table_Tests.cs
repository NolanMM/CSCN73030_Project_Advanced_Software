using Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class;

namespace DatabaseAnalysisModuleTests.Database_Tests.Input_Schema_Tables_Tests
{
    [TestClass]
    public class Feedback_Table_Tests
    {
        // Test the Feedback_Table
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
    }
}
