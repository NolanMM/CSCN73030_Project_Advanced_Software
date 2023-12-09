using Server_Side.DatabaseServices;

namespace Databbase_Tests
{
    [TestClass]
    public class DatabaseCentreTests
    {
        [TestMethod]
        public async Task ProcessDataForGetTableCorrespondingUserID_ValidInput_ReturnsResult()
        {
            // Arrange
            string UserID = "None";
            DateTime startDate = DateTime.Now.AddDays(-1);
            DateTime endDate = DateTime.Now.Date.AddTicks(-1);

            // Act
            List<ProductItemData>? result = await Database_Centre.ProcessDataForGetTableCorrespondingUserID(UserID,startDate,endDate);

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task GetDataForDatabaseServiceID_UserView_ValidInput_ReturnsResult()
        {
            // Arrange
            int TableID = 0;

            // Act
            var data = await Database_Centre.GetDataForDatabaseServiceID(TableID);

            // Assert
            Assert.IsNotNull(data);

        }

        [TestMethod]
        public async Task GetDataForDatabaseServiceID_PageView_ValidInput_ReturnsResult()
        {
            // Arrange
            int TableID = 1;

            // Act
            var data = await Database_Centre.GetDataForDatabaseServiceID(TableID);

            // Assert
            Assert.IsNotNull(data);

        }

        [TestMethod]
        public async Task GetDataForDatabaseServiceID_SaleTransaction_ValidInput_ReturnsResult()
        {
            // Arrange
            int TableID = 2;

            // Act
            var data = await Database_Centre.GetDataForDatabaseServiceID(TableID);

            // Assert
            Assert.IsNotNull(data);

        }

        [TestMethod]
        public async Task GetDataForDatabaseServiceID_Feedback_ValidInput_ReturnsResult()
        {
            // Arrange
            int TableID = 3;

            // Act
            var data = await Database_Centre.GetDataForDatabaseServiceID(TableID);

            // Assert
            Assert.IsNotNull(data);

        }

        [TestMethod]
        public async Task GetDataForDatabaseServiceID_InValidInput_ReturnsNull()
        {
            // Arrange
            int TableID = 6;

            // Act
            var data = await Database_Centre.GetDataForDatabaseServiceID(TableID);

            // Assert
            Assert.IsNull(data);

        }
    }
}