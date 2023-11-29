using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Scribble
{
    [ScribbleConstructable]
    public class TestConstructable
    {
        public TestConstructable()
        {
        }

        public TestConstructable(int value)
        {
            Value = value;
        }

        public int Value { get; set; } = 42;
    }
}
