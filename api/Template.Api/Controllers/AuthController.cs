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
    public sealed class AuthController(IMediator mediator) : ApiControllerBase(mediator)
    {
        [HttpPost(ApiRoutes.Authentication.Register)]
        public async Task<IActionResult> Create(RegisterRequest registerRequest)
        {
            var userId = await Mediator.Send(new CreateUserCommand(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password));

            return Ok(userId);
        }

        [HttpPost(ApiRoutes.Authentication.Login)]
        public IActionResult Login()
        {
            //todo
            return Ok();
        }

        [Authorize(Policy = "RefreshJwtTokenSchema")] //change to static val
        [HttpPost(ApiRoutes.Authentication.RefreshToken)]
        public IActionResult RefreshToken()
        {
            //todo
            return Ok();
        }
    }
}
