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

        int SortOrder { get; }

        bool Default { get; }
    }

    public interface IStaticDataWithFreeText
    {
        Guid Id { get; }

        bool FreeText { get; }
    }
}
