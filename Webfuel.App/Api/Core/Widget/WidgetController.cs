using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webfuel;

namespace net.app.Api
{
    [TypefuelController]
    [Route("api/[controller]")]
    public class WidgetController : ControllerBase
    {
        private readonly IWidgetService WidgetService;

        public WidgetController(IWidgetService widgetService)
        {
            WidgetService = widgetService;
        }

        [HttpGet("test")]
        [TypefuelIgnore]
        public string Test()
        {
            return "Hello!";
        }

        [HttpGet("{widgetId:guid}")]
        public async Task<Widget> Get(Guid widgetId)
        {
            return await WidgetService.RequireWidgetAsync(widgetId: widgetId);
        }

        [HttpPost("")]
        public async Task<Widget> Insert([FromBody]Widget widget)
        {
            return await WidgetService.InsertWidgetAsync(widget: widget);
        }

        [HttpPut("")]
        public async Task<Widget> Update([FromBody]Widget widget)
        {
            return await WidgetService.UpdateWidgetAsync(widget: widget);
        }

        [HttpDelete("{widgetId:guid}")]
        public async Task Delete(Guid widgetId)
        {
            await WidgetService.DeleteWidgetAsync(widgetId: widgetId);
        }

        [HttpPost("query")]
        [TypefuelAction(RetryCount = 3)]
        public async Task<QueryResult<Widget>> Query([FromBody]SimpleQuery query)
        {
            return await WidgetService.QueryWidgetAsync(query: query);
        }
    }
}