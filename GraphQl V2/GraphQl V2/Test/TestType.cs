using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl_V2.Test
{
    public class TestType : ObjectGraphType<Test>
    {
        public TestType()
        {
            Name = "test";
            Field(_ => _.Id);
            Field(_ => _.text);
            Field(_ => _.Date);
        }
    }
}
