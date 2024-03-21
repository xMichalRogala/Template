using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Template.Api.Controllers.Abstract
{
    public abstract class ApiControllerBase(IMediator mediator) : ControllerBase
    {
        protected IMediator Mediator { get; } = mediator;
    }
}
