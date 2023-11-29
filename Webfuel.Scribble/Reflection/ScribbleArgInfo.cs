using System;

namespace Webfuel.Scribble
{
    struct ScribbleArgInfo
    {
        public Type ArgumentType { get; set; }

        public string? Name { get; set; }

        public readonly static ScribbleArgInfo[] Empty = new ScribbleArgInfo[0];
    }
}
