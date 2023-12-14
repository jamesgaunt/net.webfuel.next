using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public abstract class ReportFieldAccessor
    {
        public abstract Task<object?> Get(object context, IServiceProvider services);
    }

    public class ReportFieldAccessor<TContext, TField>: ReportFieldAccessor
    {
        protected ReportFieldAccessor(Func<TContext, TField> accessor)
        {
            _accessor = accessor;
        }
        
        Func<TContext, TField> _accessor { get; }

        public override Task<object?> Get(object context, IServiceProvider services)
        {
            return Task.FromResult<object?>(_accessor((TContext)context));
        }

        public static implicit operator ReportFieldAccessor<TContext, TField>(Func<TContext, TField> accessor)
            => new ReportFieldAccessor<TContext, TField>(accessor);
    }

    public class ReportFieldAsyncAccessor<TContext, TField> : ReportFieldAccessor
    {
        protected ReportFieldAsyncAccessor(Func<TContext, Task<TField>> accessor)
        {
            _accessor = accessor;
        }

        Func<TContext, Task<TField>> _accessor { get; }

        public override async Task<object?> Get(object context, IServiceProvider services)
        {
            return await _accessor((TContext)context);
        }

        public static implicit operator ReportFieldAsyncAccessor<TContext, TField>(Func<TContext, Task<TField>> accessor)
            => new ReportFieldAsyncAccessor<TContext, TField>(accessor);
    }
}
