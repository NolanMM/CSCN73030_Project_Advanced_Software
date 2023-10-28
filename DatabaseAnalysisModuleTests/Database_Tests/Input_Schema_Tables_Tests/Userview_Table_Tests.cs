using Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class;

namespace DatabaseAnalysisModuleTests.Database_Tests.Input_Schema_Tables_Tests
{
    [TestClass]
    public class Userview_Table_Tests
    {
        // Test the Userview_Table
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
    }
}
