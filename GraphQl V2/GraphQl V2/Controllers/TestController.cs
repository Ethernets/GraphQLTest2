using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using GraphQL.Server.Transports.AspNetCore.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using GraphQL.NewtonsoftJson;

namespace GraphQl_V2.Controllers
{
    [Route("graphql")]
    public class TestController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        public TestController(ISchema schema,
        IDocumentExecuter executer)
        {
            _schema = schema;
            _executer = executer;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]
        GraphQLRequest query)
        {
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.Inputs = query.Variables?.ToInputs();

            });
            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }
            return Ok(result.Data);
        }
    }
}
