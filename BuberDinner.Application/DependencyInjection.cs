using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Query;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
            services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

            return services;
        }
    }
}
