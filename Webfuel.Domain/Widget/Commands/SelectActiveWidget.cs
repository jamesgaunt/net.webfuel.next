using Azure.Core;
using DocumentFormat.OpenXml.Office2010.Drawing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain
{
    public class SelectActiveWidget : IRequest<List<Widget>>
    {
    }

    internal class SelectActiveWidgetHandler : IRequestHandler<SelectActiveWidget, List<Widget>>
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public SelectActiveWidgetHandler(
            IWidgetRepository widgetRepository, 
            IIdentityAccessor identityAccessor)
        {
            _widgetRepository = widgetRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<List<Widget>> Handle(SelectActiveWidget request, CancellationToken cancellationToken)
        {
            var identity = _identityAccessor.User;
            if (identity == null)
                return new List<Widget>();

            return await SelectWidgets(identity.Id);
        }

        async Task<List<Widget>> SelectWidgets(Guid userId)
        {
            var result = await _widgetRepository.SelectWidgetByUserId(userId);

            if (result.Count > 10)
                return result.Take(10).ToList();

            if (result.Count > 0)
                return result;

            return await SetupDefaultWidgets(userId);
        }

        async Task<List<Widget>> SetupDefaultWidgets(Guid userId)
        {
            var result = new List<Widget>();

            result.Add(await _widgetRepository.InsertWidget(new Widget
            {
                UserId = userId,
                WidgetTypeId = WidgetTypeEnum.ProjectSummary,
                HeaderText = "Project Summary",
                SortOrder = 1
            }));

            result.Add(await _widgetRepository.InsertWidget(new Widget
            {
                UserId = userId,
                WidgetTypeId = WidgetTypeEnum.TeamSupport,
                HeaderText = "Team Support",
                SortOrder = 2
            }));

            return result;
        }
    }
}
