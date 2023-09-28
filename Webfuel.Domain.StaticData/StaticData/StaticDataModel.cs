using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.StaticData
{
    public class StaticDataModel
    {

        public IEnumerable<FundingStream> FundingStream { get; } = new List<FundingStream>();


    }
}
