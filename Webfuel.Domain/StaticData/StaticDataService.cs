using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{

    public class TestRecord
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Name { get; init; } = String.Empty;
        public string Code { get; init; } = String.Empty;
    }


    internal class StaticDataService
    {


        public void Test()
        {
            var t = new TestRecord();
        }
    }
}
