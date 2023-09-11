using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.api.Controller
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediatr;

        public AuthenticationController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(authResult.User.Id,
                                              authResult.User.FirstName,
                                              authResult.User.LastName,
                                              authResult.User.Email,
                                              authResult.Token);
        }

        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            RegisterCommand command = new(request.FirstName, request.LastName, request.Email, request.Password);
            ErrorOr<AuthenticationResult> authResult = await _mediatr.Send(command);

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }



        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            LoginQuery query = new(request.Email, request.Password);
            var authResult = await _mediatr.Send(query);

            if (authResult.IsError && authResult.FirstError == Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
            }
            return authResult.Match(authResult => Ok(MapAuthResult(authResult)),
                        errors => Problem(errors));
        }
    }
}
