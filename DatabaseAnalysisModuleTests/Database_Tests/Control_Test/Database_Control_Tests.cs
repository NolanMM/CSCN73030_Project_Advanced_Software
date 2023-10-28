using Server_Side.Database_Services.Table_Interface;
using Server_Side.Database_Services;
using static Server_Side.Services.Analysis_Report_Services;

namespace DatabaseAnalysisModuleTests.Database_Tests.Control_Test
{
    [TestClass]
    public class Database_Control_Tests
    {
        // Test the Database_Control_Tests
        [TestMethod]
        public async Task Database_Services_Control_NotificationAsync_ShouldReturnTrue()
        {
            var control = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            bool result = await control.Database_Services_Control_NotificationAsync("Test_Request", "TestControl");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Database_Services_Control_Initialize_Tables_ShouldReturnTrue()
        {
            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            bool result = await databaseControl.Initialize_Tables("Control_Initialize_Tables_Test");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Database_Services_Control_Retrieve_All_Tables_ShouldReturnTrue()
        {
            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            databaseControl.Initialize_Tables("Retrieve_All_Tables_Test").Wait();
            bool result = await databaseControl.Retrieve_All_Tables();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Database_Services_Control_Retrieve_Connection_To_TableAsync_ShouldReturnTrue()
        {
            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            Input_Tables_Template? result = await databaseControl.Retrieve_Connection_To_TableAsync("userview", "Retrieve_Connection_To_TableAsync");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Database_Services_Control_Retrieve_TableName_List_ShouldReturnTrue()
        {
            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            List<string>? result = await databaseControl.Retrieve_TableName_List();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Database_Services_Print_UserData_ShouldReturnTrue()
        {
            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            var userViews = new List<UserView>
            {
                new UserView
                {
                    User_Id = "user1",
                    Timestamp = DateTime.Now,
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now),
                },
                new UserView
                {
                    User_Id = "user2",
                    Timestamp = DateTime.Now.AddMinutes(30),
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddHours(1)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now.AddMinutes(30)),
                },
                new UserView
                {
                    User_Id = "user3",
                    Timestamp = DateTime.Now.AddMinutes(45),
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddHours(2)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now.AddMinutes(45)),
                },
                new UserView
                {
                    User_Id = "user4",
                    Timestamp = DateTime.Now.AddMinutes(60),
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddHours(3)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now.AddMinutes(60)),
                },
                new UserView
                {
                    User_Id = "user5",
                    Timestamp = DateTime.Now.AddMinutes(75),
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddHours(4)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now.AddMinutes(75)),
                },
            };
            bool? result = Database_Analysis_And_Reporting_Services_Control.Print_UserData(userViews);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Database_Services_Print_PageViewData_ShouldReturnTrue()
        {
            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            var pageViews = new List<PageView>
            {
                new PageView
                {
                    SessionId = "session1",
                    UserId = "user1",
                    PageUrl = "http://example.com/page1",
                    PageInfo = "Page 1",
                    ProductId = "product1",
                    DateTime = DateTime.Now,
                    Start_Time = DateTime.Now,
                    End_Time = DateTime.Now.AddMinutes(15),
                },
                new PageView
                {
                    SessionId = "session2",
                    UserId = "user2",
                    PageUrl = "http://example.com/page2",
                    PageInfo = "Page 2",
                    ProductId = "product2",
                    DateTime = DateTime.Now.AddMinutes(30),
                    Start_Time = DateTime.Now.AddMinutes(30),
                    End_Time = DateTime.Now.AddMinutes(45),
                },
                new PageView
                {
                    SessionId = "session3",
                    UserId = "user3",
                    PageUrl = "http://example.com/page3",
                    PageInfo = "Page 3",
                    ProductId = "product3",
                    DateTime = DateTime.Now.AddMinutes(45),
                    Start_Time = DateTime.Now.AddMinutes(45),
                    End_Time = DateTime.Now.AddMinutes(60),
                },
                new PageView
                {
                    SessionId = "session4",
                    UserId = "user4",
                    PageUrl = "http://example.com/page4",
                    PageInfo = "Page 4",
                    ProductId = "product4",
                    DateTime = DateTime.Now.AddMinutes(60),
                    Start_Time = DateTime.Now.AddMinutes(60),
                    End_Time = DateTime.Now.AddMinutes(75),
                },
                new PageView
                {
                    SessionId = "session5",
                    UserId = "user5",
                    PageUrl = "http://example.com/page5",
                    PageInfo = "Page 5",
                    ProductId = "product5",
                    DateTime = DateTime.Now.AddMinutes(75),
                    Start_Time = DateTime.Now.AddMinutes(75),
                    End_Time = DateTime.Now.AddMinutes(90),
                },
            };
            bool? result = Database_Analysis_And_Reporting_Services_Control.Print_PageViewData(pageViews);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Database_Services_Print_SaleTransactionData_ShouldReturnTrue()
        {
            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            var saleTransactions = new List<SaleTransaction>
            {
                new SaleTransaction
                {
                    TransactionId = "transaction1",
                    UserId = "user1",
                    TransactionValue = 100.50M,
                    Date = DateTime.Now,
                },
                new SaleTransaction
                {
                    TransactionId = "transaction2",
                    UserId = "user2",
                    TransactionValue = 75.25M,
                    Date = DateTime.Now.AddMinutes(30),
                },
                new SaleTransaction
                {
                    TransactionId = "transaction3",
                    UserId = "user3",
                    TransactionValue = 50.00M,
                    Date = DateTime.Now.AddMinutes(45),
                },
                new SaleTransaction
                {
                    TransactionId = "transaction4",
                    UserId = "user4",
                    TransactionValue = 125.75M,
                    Date = DateTime.Now.AddMinutes(60),
                },
                new SaleTransaction
                {
                    TransactionId = "transaction5",
                    UserId = "user5",
                    TransactionValue = 200.00M,
                    Date = DateTime.Now.AddMinutes(75),
                },
            };
            bool? result = Database_Analysis_And_Reporting_Services_Control.Print_SaleTransactionData(saleTransactions);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Database_Services_Print_FeedbackData_ShouldReturnTrue()
        {
            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            var feedbackData = new List<Feedback>
            {
                new Feedback
                {
                    FeedbackId = 1,
                    UserId = "user1",
                    ProductId = "product1",
                    StarRating = 4.5M,
                },
                new Feedback
                {
                    FeedbackId = 2,
                    UserId = "user2",
                    ProductId = "product2",
                    StarRating = 3.0M,
                },
                new Feedback
                {
                    FeedbackId = 3,
                    UserId = "user3",
                    ProductId = "product3",
                    StarRating = 5.0M,
                },
                new Feedback
                {
                    FeedbackId = 4,
                    UserId = "user4",
                    ProductId = "product4",
                    StarRating = 4.0M,
                },
                new Feedback
                {
                    FeedbackId = 5,
                    UserId = "user5",
                    ProductId = "product5",
                    StarRating = 2.5M,
                },
            };
            bool? result = Database_Analysis_And_Reporting_Services_Control.Print_FeedbackData(feedbackData);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Database_Services_Process_And_Print_Table_Data_ShouldReturnTrue()
        {
            Database_Analysis_And_Reporting_Services_Control databaseControl = Database_Analysis_And_Reporting_Services_Control.Retrieve_Database_Analysis_And_Reporting_Services_Control();
            var userViews = new List<UserView>
            {
                new UserView
                {
                    User_Id = "user1",
                    Timestamp = DateTime.Now,
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now),
                },
                new UserView
                {
                    User_Id = "user2",
                    Timestamp = DateTime.Now.AddMinutes(30),
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddHours(1)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now.AddMinutes(30)),
                },
                new UserView
                {
                    User_Id = "user3",
                    Timestamp = DateTime.Now.AddMinutes(45),
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddHours(2)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now.AddMinutes(45)),
                },
                new UserView
                {
                    User_Id = "user4",
                    Timestamp = DateTime.Now.AddMinutes(60),
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddHours(3)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now.AddMinutes(60)),
                },
                new UserView
                {
                    User_Id = "user5",
                    Timestamp = DateTime.Now.AddMinutes(75),
                    End_Date = DateOnly.FromDateTime(DateTime.Now.AddHours(4)),
                    Start_Date = DateOnly.FromDateTime(DateTime.Now.AddMinutes(75)),
                },
            };

            var pageViews = new List<PageView>
            {
                new PageView
                {
                    SessionId = "session1",
                    UserId = "user1",
                    PageUrl = "http://example.com/page1",
                    PageInfo = "Page 1",
                    ProductId = "product1",
                    DateTime = DateTime.Now,
                    Start_Time = DateTime.Now,
                    End_Time = DateTime.Now.AddMinutes(15),
                },
                new PageView
                {
                    SessionId = "session2",
                    UserId = "user2",
                    PageUrl = "http://example.com/page2",
                    PageInfo = "Page 2",
                    ProductId = "product2",
                    DateTime = DateTime.Now.AddMinutes(30),
                    Start_Time = DateTime.Now.AddMinutes(30),
                    End_Time = DateTime.Now.AddMinutes(45),
                },
                new PageView
                {
                    SessionId = "session3",
                    UserId = "user3",
                    PageUrl = "http://example.com/page3",
                    PageInfo = "Page 3",
                    ProductId = "product3",
                    DateTime = DateTime.Now.AddMinutes(45),
                    Start_Time = DateTime.Now.AddMinutes(45),
                    End_Time = DateTime.Now.AddMinutes(60),
                },
                new PageView
                {
                    SessionId = "session4",
                    UserId = "user4",
                    PageUrl = "http://example.com/page4",
                    PageInfo = "Page 4",
                    ProductId = "product4",
                    DateTime = DateTime.Now.AddMinutes(60),
                    Start_Time = DateTime.Now.AddMinutes(60),
                    End_Time = DateTime.Now.AddMinutes(75),
                },
                new PageView
                {
                    SessionId = "session5",
                    UserId = "user5",
                    PageUrl = "http://example.com/page5",
                    PageInfo = "Page 5",
                    ProductId = "product5",
                    DateTime = DateTime.Now.AddMinutes(75),
                    Start_Time = DateTime.Now.AddMinutes(75),
                    End_Time = DateTime.Now.AddMinutes(90),
                },
            };

            var saleTransactions = new List<SaleTransaction>
            {
                new SaleTransaction
                {
                    TransactionId = "transaction1",
                    UserId = "user1",
                    TransactionValue = 100.50M,
                    Date = DateTime.Now,
                },
                new SaleTransaction
                {
                    TransactionId = "transaction2",
                    UserId = "user2",
                    TransactionValue = 75.25M,
                    Date = DateTime.Now.AddMinutes(30),
                },
                new SaleTransaction
                {
                    TransactionId = "transaction3",
                    UserId = "user3",
                    TransactionValue = 50.00M,
                    Date = DateTime.Now.AddMinutes(45),
                },
                new SaleTransaction
                {
                    TransactionId = "transaction4",
                    UserId = "user4",
                    TransactionValue = 125.75M,
                    Date = DateTime.Now.AddMinutes(60),
                },
                new SaleTransaction
                {
                    TransactionId = "transaction5",
                    UserId = "user5",
                    TransactionValue = 200.00M,
                    Date = DateTime.Now.AddMinutes(75),
                },
            };

            var feedbackData = new List<Feedback>
            {
                new Feedback
                {
                    FeedbackId = 1,
                    UserId = "user1",
                    ProductId = "product1",
                    StarRating = 4.5M,
                },
                new Feedback
                {
                    FeedbackId = 2,
                    UserId = "user2",
                    ProductId = "product2",
                    StarRating = 3.0M,
                },
                new Feedback
                {
                    FeedbackId = 3,
                    UserId = "user3",
                    ProductId = "product3",
                    StarRating = 5.0M,
                },
                new Feedback
                {
                    FeedbackId = 4,
                    UserId = "user4",
                    ProductId = "product4",
                    StarRating = 4.0M,
                },
                new Feedback
                {
                    FeedbackId = 5,
                    UserId = "user5",
                    ProductId = "product5",
                    StarRating = 2.5M,
                },
            };

            var dataAsList = new List<List<object>>
            {
                userViews.Cast<object>().ToList(),
                pageViews.Cast<object>().ToList(),
                saleTransactions.Cast<object>().ToList(),
                feedbackData.Cast<object>().ToList(),
            };
            bool? result = databaseControl.Process_And_Print_Table_Data(dataAsList);
            Assert.IsTrue(result);
        }
    }
}
