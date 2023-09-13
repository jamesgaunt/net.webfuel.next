using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IWidgetService
    {
        Task<Widget> RequireWidgetAsync(Guid widgetId);

        Task<Widget> InsertWidgetAsync(Widget widget);

        Task<Widget> UpdateWidgetAsync(Widget widget);

        Task DeleteWidgetAsync(Guid widgetId);

        Task<QueryResult<Widget>> QueryWidgetAsync(Query query);
    }

    [ServiceImplementation(typeof(IWidgetService))]
    internal class WidgetService : IWidgetService
    {
        private readonly IWidgetRepository WidgetRepository;

        public WidgetService(IWidgetRepository widgetRepository)
        {
            WidgetRepository = widgetRepository;
        }

        public async Task<Widget> RequireWidgetAsync(Guid widgetId)
        {
            return await WidgetRepository.RequireWidgetAsync(widgetId);
        }

        public async Task<Widget> InsertWidgetAsync(Widget widget)
        {
            return await WidgetRepository.InsertWidgetAsync(widget);
        }

        public async Task<Widget> UpdateWidgetAsync(Widget widget)
        {
            var original = await WidgetRepository.RequireWidgetAsync(widget.Id);

            return await WidgetRepository.UpdateWidgetAsync(original: original, updated: widget);
        }

        public async Task DeleteWidgetAsync(Guid widgetId)
        {
            await WidgetRepository.DeleteWidgetAsync(widgetId);
        }

        public async Task<QueryResult<Widget>> QueryWidgetAsync(Query query)
        {
            return await WidgetRepository.QueryWidgetAsync(query);
        }
    }
}