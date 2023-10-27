using Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class;

namespace DatabaseAnalysisModuleTests.Database_Tests.Input_Schema_Tables_Tests
{
    [TestClass]
    public class Saletransaction_Table_Tests
    {
        // Test the Saletransaction_Table
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
    }
}
