using Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class;

namespace DatabaseAnalysisModuleTests.Database_Tests.Input_Schema_Tables_Tests
{
    [TestClass]
    public class Pageview_Table_Tests
    {
        // Test the Pageview_Table
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
    }
}
