using Server_Side.Database_Services;
using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;
using Server_Side.Services;

namespace DatabaseAnalysisModuleTests
{
    [TestClass]
    public class Test_Database_Services
    {
        [TestMethod]
        public void Test_Integrate_With_Feedback_Analysis_Services()
        {
            //Create an instance of the Analysis_Report_Services class
            Analysis_Report_Services analysisServices = new Analysis_Report_Services();
            bool result_InitializeData = analysisServices.InitializeData("Test_Integrate_Feedback");
            //Task.WaitAll();
            Dictionary<string, int> feedbackAnalysis = analysisServices.GetFeedbackAnalysis();
            //Task.WaitAll();
            Assert.IsTrue(result_InitializeData);
            Assert.IsNotNull(feedbackAnalysis);
        }

    }
}
