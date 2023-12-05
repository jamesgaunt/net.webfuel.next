using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class ListHeadReport : IRequest<List<Report>>
    {
    }
}
