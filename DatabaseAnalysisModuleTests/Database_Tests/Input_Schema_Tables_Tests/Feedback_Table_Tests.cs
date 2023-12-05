using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using Server_Side.DatabaseServices.Services;

namespace FeedbackTable_Services_Tests
{
    [TestClass]
    public class Database_Feedback_Services
    {
        [TestMethod]
        public async Task GetDataService_Feedback_ValidInput_ReturnsResult()
        {
            // Arrange
            FeedbackTableService Feedback_table_Service = new FeedbackTableService();

            // Act
            List<Group_1_Record_Abstraction>? result = await Feedback_table_Service.GetDataServiceAsync();

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void ProcessFeedbackList_ValidInput_ReturnsResult()
        {
            // Arrange
            FeedbackTableService Feedback_table_Service = new FeedbackTableService();
            string UserID = "None";
            List<Feedback> Feedback_Lists = new List<Feedback>
            {
                new Feedback
                {
                    Feedback_ID = 1,
                    User_ID = "user1",
                    Product_ID = "product1",
                    Stars_Rating = 4.5m,
                    Date_Updated = DateTime.Now.AddDays(-10)
                },
                new Feedback
                {
                    Feedback_ID = 2,
                    User_ID = "user2",
                    Product_ID = "product2",
                    Stars_Rating = 1.5m,
                    Date_Updated = DateTime.Now.AddDays(-15)
                }
            };

            // Act
            Dictionary<(string, string), string>? result = Feedback_table_Service.ProcessFeedbackList(Feedback_Lists);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ValidateDataAnnotations_Feedback_ValidInput_ReturnsResult()
        {
            // Arrange
            Feedback datainput = new Feedback
            {
                Feedback_ID = 1,
                User_ID = "user1",
                Product_ID = "product1",
                Stars_Rating = 4.5m,
                Date_Updated = DateTime.Now.AddDays(-10)
            };

            // Act
            bool results = FeedbackTableService.ValidateDataAnnotations(datainput);

            // Assert
            Assert.IsTrue(results);
        }

        [TestMethod]
        public void ValidateDataAnnotations_Feedback_InValidInput_ReturnsFalse()
        {
            // Arrange
            Feedback datainput = new Feedback
            {
                Feedback_ID = 2,
                User_ID = "user2user2user2user2user2user2user2user2user2user2user2user2",
                Product_ID = "product2",
                Stars_Rating = 3.0m,
                Date_Updated = DateTime.Now.AddDays(-5)
            };

            // Act
            bool results = FeedbackTableService.ValidateDataAnnotations(datainput);

            // Assert
            Assert.IsFalse(results);
        }
    }
}
