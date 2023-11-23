using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;

namespace Webfuel
{
    public interface IErrorLogService
    {
        Task LogException(string summary, Exception ex);
    }

    [Service(typeof(IErrorLogService))]
    internal class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogRepository _errorLogRepository;

        public ErrorLogService(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public async Task LogException(string summary, Exception ex)
        {
            await _errorLogRepository.InsertErrorLog(new ErrorLog
            {
                Summary = summary,
                Message = ex.Message,
                CreatedAt = DateTimeOffset.UtcNow,
            });
        }
    }
}