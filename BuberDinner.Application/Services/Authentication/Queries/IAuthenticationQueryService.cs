using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Query
{
    public interface IAuthenticationQueryService
    {
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}
