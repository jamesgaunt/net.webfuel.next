using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webfuel.App.Pages
{
    public class RequestFormModel : PageModel
    {
        private readonly IMediator _mediator;

        public RequestFormModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void OnGet()
        {
        }
    }
}
