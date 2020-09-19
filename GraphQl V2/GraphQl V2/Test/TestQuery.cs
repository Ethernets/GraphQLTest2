using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl_V2.Test
{
    public class TestQuery : ObjectGraphType
    {
        public TestQuery() 
        {
            Field<TestType>(
                "test",
                resolve: context => new Test { Id = 1, text = "ку-ку мазафака", Date = DateTime.Now });
        }
    }
}
