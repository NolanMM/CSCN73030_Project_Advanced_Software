using Server_Side.GraphQL;
using Server_Side.Controllers;
using Server_Side.Services;

namespace Analysis_Report_Module
{
    public class Source
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Source code running...\n\n");

            //DataWarehouse_Analysis_Reports_Services dataWarehouse_Analysis_Reports_Services = new DataWarehouse_Analysis_Reports_Services();
            GraphQL_Schemas graphQL_Schemas = new GraphQL_Schemas();
            GraphQL_Controllers graphQL_Controllers = new GraphQL_Controllers();
            Analysis_Report_Center analysis_Report_Services = new Analysis_Report_Center();

            //dataWarehouse_Analysis_Reports_Services.Test_DataWarehouse_Analysis_Reports_Services();
            graphQL_Schemas.GraphQL_Schemas_Services();
            graphQL_Controllers.Test_GraphQL_Controllers_Services();
        }
    }
}