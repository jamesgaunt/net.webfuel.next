using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public static class FilterUtility
    {


        public static Int32? ExtractInt32(string input)
        {
            input = new String(input.Where(c => char.IsNumber(c)).ToArray());
            if (String.IsNullOrEmpty(input))
                return null;

            if(!Int32.TryParse(input, out var result))
                return null;
            return result;
        }

    }
}
