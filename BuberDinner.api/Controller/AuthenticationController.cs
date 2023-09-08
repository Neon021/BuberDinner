using BuberDinner.Application.Services.Authentication;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Query;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.api.Controller
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationCommandService _authenticationCommandService;
        private readonly IAuthenticationQueryService _authenticationQueryService;
        public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
        {
            _authenticationCommandService = authenticationCommandService;
            _authenticationQueryService = authenticationQueryService;
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
        public IActionResult Register(RegisterRequest request)
        {
            ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }



        [Route("login")]
        public IActionResult Login(LoginRequest request)
        {
            ErrorOr<AuthenticationResult> authResult = _authenticationQueryService.Login(
                request.Email,
                request.Password);

            if (authResult.IsError && authResult.FirstError == BuberDinner.Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
            }
            return authResult.Match(authResult => Ok(MapAuthResult(authResult)),
                        errors => Problem(errors));
        }
    }
}
