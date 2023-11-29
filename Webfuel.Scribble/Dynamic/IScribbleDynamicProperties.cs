using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Scribble
{
    public interface IScribbleDynamicProperties
    {
        // Dynamic Property Binder

        object? GetProperty(string name);

        object? SetProperty(string name, object? value);
    }
}
