using Microsoft.AspNetCore.Mvc;
using GraphQL;
using GraphQL.Types;
using GraphQL.Transport;

namespace Server_Side.Controllers
{
    [ApiController]
    [Route("graphql")]
    /* This class for GraphQL controllers and endpoints.*/
    public class GraphQL_Controllers
    {
        // Create IDocumentExecuter and ISchema objects to handle incoming GraphQL requests.
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        // Constructor for GraphQL_Controllers class to initialize IDocumentExecuter and ISchema objects for handling incoming GraphQL requests.
        public GraphQL_Controllers(IDocumentExecuter documentExecuter, ISchema schema)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        // This Constructor is for testing purposes only. - Minh Nguyen
        public GraphQL_Controllers()
        {
            _documentExecuter = _documentExecuter;
            _schema = _schema;
        }

        /* Sample HttpPost method
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest request)
        {
            // Handle incoming GraphQL request, execute resolvers, and respond to the client.
            // Example code for executing queries and mutations goes here.
        }
        */

        public void Test_GraphQL_Controllers_Services()
        {
            Console.WriteLine("GraphQL_Controllers Module Connected with Source file");
        }
    }
}
