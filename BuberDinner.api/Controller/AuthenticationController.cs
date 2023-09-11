using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.api.Controller
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediatr;
        private readonly IMapper _mapper;

        public AuthenticationController(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            RegisterCommand command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> authResult = await _mediatr.Send(command);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }



        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            LoginQuery query = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediatr.Send(query);

            if (authResult.IsError && authResult.FirstError == Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
            }
            return authResult.Match(authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                        errors => Problem(errors));
        }
    }
}
