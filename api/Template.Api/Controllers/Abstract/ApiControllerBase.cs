using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Template.Api.Controllers.Abstract
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase(IMediator mediator) => Mediator = mediator;

        protected IMediator Mediator { get; }
    }
}
