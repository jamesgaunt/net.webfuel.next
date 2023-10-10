using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.StaticData
{
    public interface IStaticData
    {
        Guid Id { get; }

        string Name { get; }

        string Code { get; }

        int SortOrder { get; }

        bool Hidden { get; }

        bool Default { get; }
    }
}
