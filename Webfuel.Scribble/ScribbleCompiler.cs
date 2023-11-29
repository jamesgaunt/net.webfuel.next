using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Webfuel.Scribble
{
    public static class ScribbleCompiler
    {
        public static ScribbleExpression<T> CompileExpression<T>(string source)
        {
            var expr = ScribbleParser.ParseExpression(source);

            var type = expr.Bind(new ScribbleBindingContext<T>());
            if (type == Reflection.UnknownType)
                throw new InvalidOperationException("Unable to resolve expression type");

            return new ScribbleExpression<T>(expr);
        }

        public static ScribbleScript<T> CompileScript<T>(string source)
        {
            var statements = ScribbleParser.ParseStatements(source);

            var context = new ScribbleBindingContext<T>();
            foreach (var statement in statements)
                statement.Bind(context);

            return new ScribbleScript<T>(statements, context.AsyncValidatorInvokes);
        }

        public static ScribbleTemplate<T> CompileTemplate<T>(string source)
        {
            var statements = ScribbleParser.ParseTemplate(source);

            var context = new ScribbleBindingContext<T>();
            foreach (var statement in statements)
                statement.Bind(context);

            return new ScribbleTemplate<T>(statements, context.AsyncValidatorInvokes);
        }

        public static void RegisterAssembly(Assembly assembly)
        {
            Reflection.RegisterExtensionMethodAssembly(assembly);
            Reflection.RegisterConstructableAssembly(assembly);
        }
    }
}
