using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    internal class QueryToken
    {
        private QueryToken()
        {
        }

        public int Index { get; set; } = -1;

        public int Length { get; set; } = -1;

        public string Type { get; set; } = String.Empty;

        // Token Pool

        static int TokenCount = 0;
        static ConcurrentBag<QueryToken> TokenPool = new ConcurrentBag<QueryToken>();

        public static QueryToken Create(int index, int length, string type)
        {
            QueryToken? token = null;

            if (!TokenPool.TryTake(out token))
            {
                token = new QueryToken();
                TokenCount++;
            }

            token.Index = index;
            token.Length = length;
            token.Type = type;

            return token;
        }

        public void Release()
        {
            Index = -1;
            Length = -1;
            Type = String.Empty;
            TokenPool.Add(this);
        }
    }
}
