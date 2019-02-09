using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RpMan.WebApi.Controllers
{
    [ApiController]
    // [Route("api/[controller]/[action]")]
    public abstract class RpManControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator
        {
            get
            {
                return _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
            }
        }
    }
}
