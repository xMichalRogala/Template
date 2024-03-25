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
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            var tokenResponse = await Mediator.Send(new CreateUserCommand(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password), cancellationToken);

            return Ok(tokenResponse);
        }

        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var tokenResult = await Mediator.Send(loginRequest, cancellationToken);

            return Ok(tokenResult);
        }

        [Authorize(Policy = "RefreshJwtTokenSchema")] //change to static val
        [HttpPost(ApiRoutes.Authentication.RefreshToken)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public IActionResult RefreshToken()
        {
            //todo
            return Ok();
        }

        [Authorize]
        [HttpPost(ApiRoutes.Authentication.Revoke)]
        public IActionResult Revoke()
        {
            //todo
            return Ok();
        }
    }
}
