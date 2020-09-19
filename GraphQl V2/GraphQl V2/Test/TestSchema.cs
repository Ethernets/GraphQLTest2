using GraphQL.Types;
using GraphQL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl_V2.Test
{
    public class TestSchema : Schema
    {
        public TestSchema(IServiceProvider provider):base(provider)
        {
            Query = provider.GetRequiredService<TestQuery>();
        }
    }
}
