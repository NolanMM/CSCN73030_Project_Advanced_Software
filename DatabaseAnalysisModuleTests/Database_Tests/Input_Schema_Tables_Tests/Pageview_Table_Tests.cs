using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using Server_Side.DatabaseServices.Services;

namespace PageViewTable_Services_Tests
{
    [TestClass]
    public class Database_Pageview_Services
    {
        [TestMethod]
        public async Task GetDataService_Pageview_ValidInput_ReturnsResult()
        {
            // Arrange
            PageViewTableService Pageview_table_services = new PageViewTableService();

            // Act
            List<Group_1_Record_Abstraction>? result = await Pageview_table_services.GetDataServiceAsync();

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void ProcessPageViewList_ValidInput_ReturnsResult()
        {
            // Arrange
            PageViewTableService Pageview_table_services = new PageViewTableService();
            string UserID = "None";
            List<PageView> PageView_Lists = new List<PageView>
            {
                new PageView
                {
                    PageView_ID = 1,
                    Page_Name = "Home",
                    Page_Info = "Welcome to our website",
                    Product_ID = "P001",
                    Start_Time = DateTime.Now.AddDays(-7),
                    UserID = "user1"
                },
                new PageView
                {
                    PageView_ID = 2,
                    Page_Name = "Product",
                    Page_Info = "Details about Product A",
                    Product_ID = "P002",
                    Start_Time = DateTime.Now.AddDays(-5),
                    UserID = "user2"
                },
            };

            // Act
            Dictionary<(string, string), string>? result = Pageview_table_services.ProcessPageViewList(PageView_Lists, UserID);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ValidateDataAnnotations_PageView_ValidInput_ReturnsResult()
        {
            // Arrange
            PageView datainput = new PageView
            {
                PageView_ID = 3,
                Page_Name = "Contact",
                Page_Info = "Get in touch with us",
                Product_ID = "P003",
                Start_Time = DateTime.Now.AddDays(-3),
                UserID = "user3"
            };

            // Act
            bool results = PageViewTableService.ValidateDataAnnotations(datainput);

            // Assert
            Assert.IsTrue(results);
        }

        [TestMethod]
        public void ValidateDataAnnotations_PageView_InValidInput_ReturnsFalse()
        {
            // Arrange
            PageView datainput = new PageView
            {
                // Invalid input: UserID exceeds the maximum length
                PageView_ID = 4,
                Page_Name = "Invalid Page",
                Page_Info = "Invalid Page Info",
                Product_ID = "P004",
                Start_Time = DateTime.Now,
                UserID = "user4user4user4user4user4user4user4user4user4user4user4user4user4"
            };
            // Act
            bool results = PageViewTableService.ValidateDataAnnotations(datainput);

            // Assert
            Assert.IsFalse(results);
        }
    }
}
