namespace BuberDinner.Application.Services.Authentication.Query
{
    public interface IAuthenticationQueryService
    {
        AuthenticationResult Login(string email, string password);
    }
}
