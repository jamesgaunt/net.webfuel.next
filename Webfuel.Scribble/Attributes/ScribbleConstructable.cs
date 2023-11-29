using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Scribble
{
    /// <summary>
    /// Indicates that this class can be constructed by Scribble scripts
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ScribbleConstructableAttribute: Attribute
    {
    }
}
