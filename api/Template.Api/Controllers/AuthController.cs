using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Contracts;
using Template.Api.Controllers.Abstract;
using Template.Application.Users.Commands.CreateUser;
using Template.Contracts.Auth;

namespace Template.Api.Controllers
{
    [AllowAnonymous]
    public sealed class AuthController : ApiControllerBase
    {
        public AuthController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost(ApiRoutes.Authentication.Register)]
        public async Task<IActionResult> Create(RegisterRequest registerRequest)
        {
            var userId = await Mediator.Send(new CreateUserCommand(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password));

            return Ok(userId);
        }
}
}
