using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using Server_Side.DatabaseServices.Services;

namespace SaleTransactionTable_Services_Tests
{
    [TestClass]
    public class Database_SaleTransactions_Services
    {
        [TestMethod]
        public async Task GetDataService_SaleTransaction_ValidInput_ReturnsResult()
        {
            // Arrange
            SaleTransactionTableService SaleTransaction_table_services = new SaleTransactionTableService();

            // Act
            List<Group_1_Record_Abstraction>? result = await SaleTransaction_table_services.GetDataServiceAsync();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProcessSaleTransactionList_ValidInput_ReturnsResult()
        {
            // Arrange
            SaleTransactionTableService SaleTransaction_table_services = new SaleTransactionTableService();
            string UserID = "None";
            List<SaleTransaction> SaleTransaction_Lists = new List<SaleTransaction>
            {
                new SaleTransaction
                {
                    Transaction_ID = "T001",
                    User_ID = "user1",
                    Order_Value = 150.50m,
                    date = DateTime.Now.AddDays(-7),
                    Details_Products = "[{\"Product_ID\": 1, \"Product_Price\": 120.25, \"Product_Quantity\": 1}," +
                                        "{\"Product_ID\": 132, \"Product_Price\": 120.25, \"Product_Quantity\": 2}," +
                                        "{\"Product_ID\": 231, \"Product_Price\": 221.25, \"Product_Quantity\": 6}]"
                },
            };

            // Act
            Dictionary<(string, string), string>? result = SaleTransaction_table_services.ProcessSaleTransactionList(SaleTransaction_Lists, UserID);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ValidateDataAnnotations_SaleTransaction_ValidInput_ReturnsResult()
        {
            SaleTransaction datainput = new SaleTransaction
            {
                Transaction_ID = "T003",
                User_ID = "user3",
                Order_Value = 200.00m,
                date = DateTime.Now.AddDays(-3),
                Details_Products = "Product6, Product7, Product8"
            };

            // Act
            bool results = SaleTransactionTableService.ValidateDataAnnotations(datainput);

            // Assert
            Assert.IsTrue(results);
        }

        [TestMethod]
        public void ValidateDataAnnotations_SaleTransaction_InValidInput_ReturnsFalse()
        {
            // Arrange
            SaleTransaction datainput = new SaleTransaction
            {
                Transaction_ID = "T004",
                User_ID = "user4",
                Order_Value = -5.0m,
                date = DateTime.Now,
                Details_Products = "Product9, Product10"
            };

            // Act
            bool results = SaleTransactionTableService.ValidateDataAnnotations(datainput);

            // Assert
            Assert.IsFalse(results);
        }
    }
}
