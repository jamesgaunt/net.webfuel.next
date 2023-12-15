using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public interface ICleanupProvider
    {
        Task<int> CleanupAsync();
    }
}
