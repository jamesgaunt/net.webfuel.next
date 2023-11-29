using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleStatementForEach : ScribbleStatement
    {
        private ScribbleStatementForEach(ScribbleExprIdentifier identifier, ScribbleExpr collection, ScribbleStatement body)
        {
            Identifier = identifier;
            Collection = collection;
            Body = body;
        }

        public ScribbleExprIdentifier Identifier { get; private set; }

        public ScribbleExpr Collection { get; private set; }

        public ScribbleStatement Body { get; private set; }

        public static ScribbleStatementForEach Create(ScribbleExprIdentifier identifier, ScribbleExpr collection, ScribbleStatement body)
        {
            return new ScribbleStatementForEach(identifier, collection, body);
        }

        public override string ToString()
        {
            return $"foreach (var {Identifier} in {Collection}) {Body}";
        }

        internal override void Bind<T>(ScribbleBindingContext<T> context)
        {
            var type = Collection.Bind(context);
            if (!Reflection.CanCast(type, typeof(IEnumerable)))
                throw new InvalidOperationException($"foreach statements cannot operate on variables of type '{type.Name}'");

            var index_name = Identifier.Name + "_Index";

            context.Scope.PushScope();
            context.Scope.DeclareVar(Identifier.Name, Reflection.GetEnumerableElementType(type) ?? Reflection.UnknownType, null);
            context.Scope.DeclareVar(index_name, typeof(Int32), 0);
            {
                Body.Bind(context);
            }
            context.Scope.PopScope();
        }

        internal override async Task ExecuteAsync<T>(ScribbleExecutionContext<T> context)
        {
            var collection = await context.EvaluateExpressionAsync(Collection);
            if (collection == null)
                throw new InvalidOperationException("foreach collection evaluated to null");

            if (collection is IEnumerable)
            {
                var enumerable = (IEnumerable)collection;
                var index_name = Identifier.Name + "_Index";

                context.Scope.PushScope();
                context.Scope.DeclareVar(Identifier.Name, Reflection.GetEnumerableElementType(collection.GetType()) ?? Reflection.UnknownType, null);
                context.Scope.DeclareVar(index_name, typeof(Int32), 0);
                {
                    var index = 0;
                    foreach (var item in enumerable)
                    {
                        context.SetProperty(Identifier.Name, item);
                        context.SetProperty(index_name, index);
                        await Body.ExecuteAsync(context);
                        index++;
                    }
                }
                context.Scope.PopScope();
            }
            else
            {
                throw new InvalidOperationException($"foreach statments cannot operate on variables of type '{collection.GetType()}'");
            }
        }
    }
}
