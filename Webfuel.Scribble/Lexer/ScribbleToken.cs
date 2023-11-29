using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    internal class ScribbleToken
    {
        private ScribbleToken()
        {
        }

        public int Index { get; set; } = -1;

        public int Length { get; set; } = -1;

        public string Type { get; set; } = String.Empty;

        // Token Pool

        static int TokenCount = 0;
        static ConcurrentBag<ScribbleToken> TokenPool = new ConcurrentBag<ScribbleToken>();

        public static ScribbleToken Create(int index, int length, string type)
        {
            ScribbleToken? token = null;

            if (!TokenPool.TryTake(out token))
            {
                token = new ScribbleToken();
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
