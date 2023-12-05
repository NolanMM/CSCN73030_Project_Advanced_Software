using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using Server_Side.DatabaseServices.Services;

namespace UserviewTable_Services_Tests
{
    [TestClass]
    public class Database_Userview_Services
    {
        [TestMethod]
        public async Task GetDataService_UserView_ValidInput_ReturnsResult()
        {
            // Arrange
            UserViewTableService Userview_table_services = new UserViewTableService();

            // Act
            List<Group_1_Record_Abstraction>? result = await Userview_table_services.GetDataServiceAsync();

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void ProcessUserViewList_ValidInput_ReturnsResult()
        {
            // Arrange
            UserViewTableService Userview_table_services = new UserViewTableService();
            string UserID = "None";
            List<UserView> UserView_Lists = new List<UserView>
            {
                new UserView
                {
                    UserView_ID = 1,
                    User_ID = "user1",
                    Product_ID = "product1",
                    Time_Count = 2.5m,
                    Date_Access = DateTime.Now.AddDays(-5)
                },
                new UserView
                {
                    UserView_ID = 2,
                    User_ID = "user2",
                    Product_ID = "product2",
                    Time_Count = 1.8m,
                    Date_Access = DateTime.Now.AddDays(-3)
                },
            };


            // Act
            Dictionary<(string, string), string>? result = Userview_table_services.ProcessUserViewList(UserView_Lists, UserID);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ValidateDataAnnotations_Userview_ValidInput_ReturnsResult()
        {
            // Arrange
            UserViewTableService Userview_table_services = new UserViewTableService();
            UserView datainput = new UserView
            {
                UserView_ID = 1,
                User_ID = "user1",
                Product_ID = "product1",
                Time_Count = 2.5m,
                Date_Access = DateTime.Now.AddDays(-5)
            };

            // Act
            bool results = UserViewTableService.ValidateDataAnnotations(datainput);

            // Assert
            Assert.IsTrue(results);
        }

        [TestMethod]
        public void ValidateDataAnnotations_Userview_InValidInput_ReturnsFalse()
        {
            // Arrange
            UserView datainput = new UserView
            {
                UserView_ID = 1,
                Product_ID = "product1",
                Time_Count = 2.5m,
                Date_Access = DateTime.Now.AddDays(-5)
            };

            // Act
            bool results = UserViewTableService.ValidateDataAnnotations(datainput);

            // Assert
            Assert.IsFalse(results);
        }
    }
}
