using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain
{
    public class CreateWidget : IRequest<Widget>
    {
        public required Guid WidgetTypeId { get; set; }
    }

    internal class CreateWidgetHandler : IRequestHandler<CreateWidget, Widget>
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IServiceProvider _serviceProvider;

        public CreateWidgetHandler(
            IWidgetRepository widgetRepository,
            IIdentityAccessor identityAccessor,
            IServiceProvider serviceProvider)
        {
            _widgetRepository = widgetRepository;
            _identityAccessor = identityAccessor;
            _serviceProvider = serviceProvider;
        }

        public async Task<Widget> Handle(CreateWidget request, CancellationToken cancellationToken)
        {
            var identity = _identityAccessor.User;
            if (identity == null)
                throw new InvalidOperationException("Invalid identity context");

            var existing = await _widgetRepository.SelectWidgetByUserId(identity.Id);
            if (existing.Count() >= 10)
                throw new InvalidOperationException("Maximum number of widgets exceeded");

            var widget = new Widget
            {
                UserId = identity.Id,
                WidgetTypeId = request.WidgetTypeId,
                SortOrder = existing.Count(),
            };

            var provider = _serviceProvider.GetRequiredKeyedService<IWidgetProvider>(widget.WidgetTypeId);

            if (!await provider.AuthoriseAccess())
                throw new NotAuthorizedException();

            widget = await provider.Initialise(widget);

            return await _widgetRepository.InsertWidget(widget);
        }
    }
}
